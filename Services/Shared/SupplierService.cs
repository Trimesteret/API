using API.Models.Suppliers;
using API.DataTransferObjects;
using API.Enums;
using API.Models;
using API.Models.Items;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace API.Services.Shared;

public class SupplierService : ISupplierService
{
    private readonly SharedContext _sharedContext;
    
    public SupplierService(SharedContext dbSharedContext)
    {
        _sharedContext = dbSharedContext;
    }
    
    public async Task<Supplier> CreateSupplier(SupplierDto supplierDto)
    {
        Supplier supplier = new Supplier(supplierDto.Name, supplierDto.Items);
        await _sharedContext.Suppliers.AddAsync(supplier);
        await _sharedContext.SaveChangesAsync();
        return supplier;

    }

    public async Task<List<Supplier>> GetAllSuppliers()
    {
        IQueryable<Supplier> query = _sharedContext.Suppliers;
        return await query.Take(4).ToListAsync();
    }
}

