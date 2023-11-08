using MediatR;
using Social.Application.Models;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.Queries
{
    public class GetUserProfileById : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
