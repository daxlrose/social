﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Social.Application.Enums;
using Social.Application.Identity.Commands;
using Social.Application.Models;
using Social.Application.Options;
using Social.Application.Services;
using Social.Dal;
using Social.Domain.Aggregates.UserProfileAggregate;
using Social.Domain.Exceptions;

namespace Social.Application.Identity.CommandHandlers
{
    internal class RegisterIdentityHandler : IRequestHandler<RegisterIdentity, OperationResult<string>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;

        public RegisterIdentityHandler(DataContext ctx, UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> jwtSettings, IdentityService identityService)
        {
            _ctx = ctx;
            _userManager = userManager;
            _identityService = identityService;
        }

        public async Task<OperationResult<string>> Handle(RegisterIdentity request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<string>();
            try
            {
                var creationValidated = await ValidateIdentityDoesNotExist(result, request);
                if (!creationValidated) return result;

                await using var transaction = await _ctx.Database.BeginTransactionAsync(cancellationToken);

                var identity = await CreateIdentityUserAsync(result, request, transaction, cancellationToken);
                if (identity == null) return result;

                var profile = await CreateUserProfileAsync(result, request, transaction, identity, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                result.Payload = GetJwtString(identity, profile);
                return result;
            }

            catch (UserProfileNotValidException ex)
            {
                result.IsError = true;
                ex.ValidationErrors.ForEach(e =>
                {
                    var error = new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = $"{ex.Message}"
                    };
                    result.Errors.Add(error);
                });
            }

            catch (Exception e)
            {
                var error = new Error
                {
                    Code = ErrorCode.UnknownError,
                    Message = $"{e.Message}"
                };
                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }

        private async Task<bool> ValidateIdentityDoesNotExist(OperationResult<string> result,
        RegisterIdentity request)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.Username);

            if (existingIdentity != null)
            {
                result.IsError = true;
                var error = new Error
                {
                    Code = ErrorCode.IdentityUserAlreadyExists,
                    Message = $"Provided email address already exists. Cannot register new user"
                };
                result.Errors.Add(error);
                return false;
            }

            return true;
        }

        private async Task<IdentityUser> CreateIdentityUserAsync(OperationResult<string> result,
            RegisterIdentity request, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var identity = new IdentityUser { Email = request.Username, UserName = request.Username };
            var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
            if (!createdIdentity.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);
                result.IsError = true;

                foreach (var identityError in createdIdentity.Errors)
                {
                    var error = new Error
                    {
                        Code = ErrorCode.IdentityCreationFailed,
                        Message = identityError.Description
                    };
                    result.Errors.Add(error);
                }
                return null;
            }

            return identity;
        }

        private async Task<UserProfile> CreateUserProfileAsync(OperationResult<string> result,
            RegisterIdentity request, IDbContextTransaction transaction, IdentityUser identity, CancellationToken cancellationToken)
        {
            try
            {
                var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.Username,
                    request.Phone, request.DateOfBirth, request.CurrentCity);

                var profile = UserProfile.CreateUserProfile(identity.Id, profileInfo);
                _ctx.UserProfiles.Add(profile);
                await _ctx.SaveChangesAsync(cancellationToken);
                return profile;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private string GetJwtString(IdentityUser identity, UserProfile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identity.Email),
            new Claim("IdentityId", identity.Id),
            new Claim("UserProfileId", profile.Id.ToString())
            });

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }
    }
}
