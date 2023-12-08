using API.Models.Suppliers;
using API.DataTransferObjects;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class SupplierService : ISupplierService
{
    private readonly SharedContext _sharedContext;
    private readonly IMapper _mapper;

    public SupplierService(SharedContext dbSharedContext, IMapper mapper)
    {
        _sharedContext = dbSharedContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets a supplier by id and returns a SupplierDto
    /// </summary>
    /// <param name="id">The supplier id</param>
    /// <returns></returns>
    /// <exception cref="Exception">If supplier could not be found</exception>
    public async Task<SupplierDto> GetSupplierById(int id)
    {
        Supplier? supplier = await _sharedContext.Suppliers.FirstOrDefaultAsync(i => i.Id == id);
        if (supplier == null)
        {
            throw new Exception("Supplier could not be found");
        }

        var supplierDto = _mapper.Map<SupplierDto>(supplier);
        var associated = await supplier.GetAssociatedItems(_sharedContext);
        supplierDto.Items = _mapper.Map<List<ItemDto>>(associated);

        return supplierDto;
    }

    /// <summary>
    /// Edit a supplier given a supplierDto
    /// </summary>
    /// <param name="supplierDto">The new values for the supplier</param>
    /// <returns>The edited supplier as a supplierDto</returns>
    /// <exception cref="Exception">If the supplier is not found</exception>
    public async Task<SupplierDto> EditSupplier(SupplierDto supplierDto)
    {
        var supplier = await _sharedContext.Suppliers.FirstOrDefaultAsync(supplier => supplier.Id == supplierDto.Id);
        if (supplier == null)
        {
            throw new Exception("Supplier could not be found");
        }
        supplier.ChangeSupplierProperties(supplierDto.Name);
        await supplier.SetAssociatedItems(_sharedContext, supplierDto.Items);
        await _sharedContext.SaveChangesAsync();
        supplierDto = _mapper.Map<SupplierDto>(supplier);
        var associatedItems = await supplier.GetAssociatedItems(_sharedContext);
        supplierDto.Items = _mapper.Map<List<ItemDto>>(associatedItems);
        return supplierDto;
    }

    /// <summary>
    /// Deletes the supplier with an id
    /// </summary>
    /// <param name="id">The id to delete supplier by</param>
    /// <exception cref="Exception">If the supplier could not be found</exception>
    public async Task DeleteSupplier(int id)
    {
        var existingSupplier = await _sharedContext.Suppliers.FirstOrDefaultAsync(supplier => supplier.Id == id);

        if (existingSupplier == null)
        {
            throw new Exception("Supplier could not be found");
        }

        await existingSupplier.ClearAssociatedItems(_sharedContext);

        _sharedContext.Suppliers.Remove(existingSupplier);

        await _sharedContext.SaveChangesAsync();
    }

    /// <summary>
    /// Create a new supplier given a supplierDto
    /// </summary>
    /// <param name="supplierDto">The supplierDto with the new values</param>
    /// <returns>The newly created supplier as a supplierDto</returns>
    public async Task<SupplierDto> CreateSupplier(SupplierDto supplierDto)
    {
        Supplier supplier = new Supplier(supplierDto.Name);

        _sharedContext.Suppliers.Add(supplier);
        await _sharedContext.SaveChangesAsync();

        return _mapper.Map<SupplierDto>(supplier);
    }

    /// <summary>
    /// Gets all suppliers
    /// </summary>
    /// <returns>A list of all suppliers</returns>
    public async Task<List<Supplier>> GetAllSuppliers()
    {
        return await _sharedContext.Suppliers.ToListAsync();
    }
}
