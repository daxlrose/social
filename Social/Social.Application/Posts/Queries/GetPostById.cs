using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.Queries
{
    public class GetPostById : IRequest<OperationResult<Post>>
    {
        public Guid PostId { get; set; }
    }
}
