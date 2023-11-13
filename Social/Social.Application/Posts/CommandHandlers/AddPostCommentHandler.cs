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
    internal class AddPostCommentHandler : IRequestHandler<AddPostComment, OperationResult<PostComment>>
    {
        private readonly DataContext _ctx;

        public AddPostCommentHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<PostComment>> Handle(AddPostComment request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostComment>();

            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken: cancellationToken);
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

                var comment = PostComment.CreatePostComment(request.PostId, request.CommentText, request.UserProfileId);

                post.AddPostComment(comment);

                _ctx.Posts.Update(post);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = comment;

            }

            catch (PostCommentNotValidException e)
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
