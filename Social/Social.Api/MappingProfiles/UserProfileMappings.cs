using AutoMapper;
using Social.Api.Contracts.UserProfiles.Requests;
using Social.Api.Contracts.UserProfiles.Responses;
using Social.Application.UserProfiles.Commands;
using Social.Domain.Aggregates.UserProfileAggregate;

namespace Social.Api.MappingProfiles
{
    public class UserProfileMappings : Profile
    {
        public UserProfileMappings()
        {
            CreateMap<UserProfileCreateUpdate, UpdateUserProfileBasicInfo>();
            CreateMap<UserProfile, UserProfileResponse>()
                .ForMember(dest => dest.UserProfileId, opts => opts.MapFrom(src => src.Id));
            CreateMap<BasicInfo, BasicInformation>();
        }
    }
}
