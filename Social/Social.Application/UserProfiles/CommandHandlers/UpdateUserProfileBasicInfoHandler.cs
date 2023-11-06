using MediatR;
using Microsoft.EntityFrameworkCore;
using Social.Application.UserProfiles.Commands;
using Social.Dal;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Application.UserProfiles.CommandHandlers
{
    internal class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo>
    {
        private readonly DataContext _ctx;

        public UpdateUserProfileBasicInfoHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task Handle(UpdateUserProfileBasicInfo request,
            CancellationToken cancellationToken)
        {
            var userProfile = await _ctx.UserProfiles
                .FirstOrDefaultAsync(up => up.Id == request.UserProfileId);

            var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                request.EmailAddress, request.Phone, request.DateOfBirth, request.CurrentCity);

            userProfile.UpdateBasicInfo(basicInfo);

            _ctx.UserProfiles.Update(userProfile);
            await _ctx.SaveChangesAsync();
        }
    }
}
