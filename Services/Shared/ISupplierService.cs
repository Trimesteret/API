using API.DataTransferObjects;
using API.Models.Items;
using API.Models.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Shared;

public interface ISupplierService
{
    public Task<Supplier> CreateSupplier(SupplierDto supplierDto);
    public Task<List<Supplier>> GetAllSuppliers();
    public Task<Supplier> GetSupplierById(int id);
    public Task<List<Item>> GetAssociated(int id);
}