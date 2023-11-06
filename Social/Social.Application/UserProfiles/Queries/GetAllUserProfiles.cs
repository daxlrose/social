using MediatR;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.Queries
{
    public class GetAllUserProfiles : IRequest<IEnumerable<UserProfile>>
    {
    }
}
