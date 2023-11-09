using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.Enums;
using Social.Application.Models;
using Social.Application.Posts.Queries;
using Social.Dal;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.QueryHandlers
{
    internal class GetAllPostsHandler : IRequestHandler<GetAllPosts, OperationResult<List<Post>>>
    {
        private readonly DataContext _ctx;
        public GetAllPostsHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<List<Post>>> Handle(GetAllPosts request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Post>>();
            try
            {
                var posts = await _ctx.Posts.ToListAsync();
                result.Payload = posts;
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
