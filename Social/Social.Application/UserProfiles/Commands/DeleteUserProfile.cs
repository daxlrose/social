using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.Commands
{
    public class DeleteUserProfile : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
