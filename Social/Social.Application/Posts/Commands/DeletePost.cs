using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.PostAggregate;

namespace Social.Application.Posts.Commands
{
    public class DeletePost : IRequest<OperationResult<Post>>
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
    }
}
