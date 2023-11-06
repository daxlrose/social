using MediatR;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.Queries
{
    public class GetUserProfileById : IRequest<UserProfile>
    {
        public Guid UserProfileId { get; set; }
    }
}
