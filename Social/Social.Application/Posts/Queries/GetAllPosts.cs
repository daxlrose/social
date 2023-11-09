using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.Queries
{
    public class GetAllPosts : IRequest<OperationResult<List<Post>>>
    {

    }
}
