using MediatR;

namespace Social.Application.UserProfiles.Commands
{
    public class DeleteUserProfile : IRequest
    {
        public Guid UserProfileId { get; set; }
    }
}
