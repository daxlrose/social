using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.UserProfiles.Commands;
using Social.Dal;

namespace Social.Application.UserProfiles.CommandHandlers
{
    internal class DeleteUserProfileHandler : IRequestHandler<DeleteUserProfile>
    {
        private readonly DataContext _ctx;
        public DeleteUserProfileHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task Handle(DeleteUserProfile request, CancellationToken cancellationToken)
        {
            var userProfile = await _ctx.UserProfiles
                .FirstOrDefaultAsync(up => up.Id == request.UserProfileId);

            _ctx.UserProfiles.Remove(userProfile);
            await _ctx.SaveChangesAsync();
        }
    }
}
