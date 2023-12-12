using API.DataTransferObjects;
using API.Enums;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class EnumService : IEnumService
{
    private readonly SharedContext _sharedContext;

    public EnumService(SharedContext sharedContext)
    {
        _sharedContext = sharedContext;
    }

    /// <summary>
    /// Get all custom enums of a certain type
    /// </summary>
    /// <param name="enumType">The type of enums to get</param>
    /// <returns></returns>
    public async Task<List<CustomEnum>> GetCustomEnums(EnumType enumType)
    {
        return await _sharedContext.CustomEnums.Where(cEnum => cEnum.EnumType == enumType).ToListAsync();
    }

    /// <summary>
    /// Creates a new custom enum
    /// </summary>
    /// <param name="customEnum">The custom enum to create</param>
    /// <returns></returns>
    /// <exception cref="Exception">Throws if the custom enum already exists</exception>
    public async Task<CustomEnum> CreateCustomEnum(CustomEnumDto customEnum)
    {
        var cEnum = await _sharedContext.CustomEnums.FirstOrDefaultAsync(cEnum => cEnum.Id == customEnum.Id);

        if(cEnum != null)
        {
            throw new Exception("Custom enum already exists");
        }

        cEnum = new CustomEnum(customEnum.EnumType, customEnum.Key, customEnum.Value);

        _sharedContext.CustomEnums.Add(cEnum);

        await _sharedContext.SaveChangesAsync();

        return cEnum;
    }

    /// <summary>
    /// Deletes a custom enum by an id
    /// </summary>
    /// <param name="id">The id of the enum</param>
    /// <returns></returns>
    /// <exception cref="Exception">Throws if the custom enum could not be found</exception>
    public async Task<bool> DeleteCustomEnum(int id)
    {
        var cEnum = await _sharedContext.CustomEnums.FirstOrDefaultAsync(cEnum => cEnum.Id == id);

        if(cEnum == null)
        {
            throw new Exception("Custom enum could not be found");
        }

        _sharedContext.CustomEnums.Remove(cEnum);
        await _sharedContext.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Gets an enum by an id
    /// </summary>
    /// <param name="id">The id of the enum to get</param>
    /// <returns></returns>
    /// <exception cref="Exception">Is thrown if enum could not be found</exception>
    public async Task<CustomEnum> GetCustomEnumById(int id)
    {
        var cEnum = await _sharedContext.CustomEnums.FirstOrDefaultAsync(cEnum => cEnum.Id == id);

        if(cEnum == null)
        {
            throw new Exception("Custom enum could not be found");
        }

        return cEnum;
    }
}
