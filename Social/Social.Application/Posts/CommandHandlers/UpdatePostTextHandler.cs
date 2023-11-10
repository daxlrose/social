﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.Enums;
using Social.Application.Models;
using Social.Application.Posts.Commands;
using Social.Dal;
using Social.Domain.Aggregates.PostAggregate;
using Social.Domain.Exceptions;

namespace Social.Application.Posts.CommandHandlers
{
    internal class UpdatePostTextHandler : IRequestHandler<UpdatePostText, OperationResult<Post>>
    {
        private readonly DataContext _ctx;

        public UpdatePostTextHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<Post>> Handle(UpdatePostText request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();

            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId);

                if (post is null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No post found with ID {request.PostId}"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                post.UpdatePostText(request.NewText);

                await _ctx.SaveChangesAsync();

                result.Payload = post;
            }

            catch (PostNotValidException e)
            {
                result.IsError = true;
                e.ValidationErrors.ForEach(er =>
                {
                    var error = new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = $"{e.Message}"
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
    }
}
