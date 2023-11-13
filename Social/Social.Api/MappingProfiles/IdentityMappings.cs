using AutoMapper;
using Social.Api.Contracts.Identity;
using Social.Application.Identity.Commands;

namespace Social.Api.MappingProfiles
{
    public class IdentityMappings : Profile
    {
        public IdentityMappings()
        {
            CreateMap<UserRegistration, RegisterIdentity>();
            CreateMap<Login, LoginCommand>();
        }
    }
}
