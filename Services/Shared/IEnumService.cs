using API.Enums;

namespace API.Services.Shared;

public interface IEnumService
{
    Task<List<CustomEnum>> GetCustomEnums(EnumType enumType);
}
