using API.Models;
using API.Models.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Shared;

public class ItemService : IItemService
{
    private readonly Context _context;
    
    public ItemService(Context dbContext)
    {
        _context = dbContext;
    }
    
    public async Task<List<Item>> GetItemsBySearch(string search)
    {
        if (string.IsNullOrEmpty(search)) return await _context.Items.Take(36).ToListAsync();
        return await _context.Items.Where(item => item.Name.ToLower().Contains(search.ToLower()) ||
                                                  item.Price.ToString().Contains(search)).OrderBy(item => item.Price).Take(36).ToListAsync();
    }
    
    public async Task<Item> GetItemById(int id)
    {
        return await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
    }
}
