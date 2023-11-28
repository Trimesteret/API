using AutoMapper;

namespace API.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Authentication.User, DataTransferObjects.UserStandardDto>()
            .ForMember(userStandardDto => userStandardDto.Role, user => user.MapFrom(user => user.GetClassRoleEnum()));
        CreateMap<DataTransferObjects.UserStandardDto, Models.Authentication.User>();
    }
}
