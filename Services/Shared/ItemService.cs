using API.Models;
using API.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class ItemService : IItemService
{
    private readonly DBContext _context;
    
    public ItemService(DBContext dbContext)
    {
        _context = dbContext;
    }
    
    public async Task<List<Item>> GetAllItems()
    {
        return await _context.Items.ToListAsync();
    }
    
    public async Task<Item> GetItemById(int id)
    {
        return await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
    }
}