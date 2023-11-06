using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.UserProfiles.Queries;
using Social.Dal;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.QueryHandlers
{
    internal class GetUserProfileByIdHandler : IRequestHandler<GetUserProfileById, UserProfile>
    {
        private readonly DataContext _ctx;

        public GetUserProfileByIdHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<UserProfile> Handle(GetUserProfileById request, CancellationToken cancellationToken)
        {
            return await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.Id == request.UserProfileId);
        }
    }
}
