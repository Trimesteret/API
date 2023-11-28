using AutoMapper;

namespace API.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Authentication.User, DataTransferObjects.UserStandardDto>();
        CreateMap<DataTransferObjects.UserStandardDto, Models.Authentication.User>();
    }
}
