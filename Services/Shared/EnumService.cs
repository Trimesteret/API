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

    public async Task<List<CustomEnum>> GetCustomEnums(EnumType enumType)
    {
        return await _sharedContext.CustomEnums.Where(cEnum => cEnum.EnumType == enumType).ToListAsync();
    }
}
