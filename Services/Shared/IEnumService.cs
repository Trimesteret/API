using API.DataTransferObjects;
using API.Enums;

namespace API.Services.Shared;

public interface IEnumService
{
    Task<List<CustomEnum>> GetCustomEnums(EnumType enumType);

    Task<CustomEnum> CreateCustomEnum(CustomEnumDto customEnum);

    Task<bool> DeleteCustomEnum(int id);

    Task<CustomEnum> GetCustomEnumById(int id);
}
