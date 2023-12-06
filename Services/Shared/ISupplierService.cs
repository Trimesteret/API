using API.DataTransferObjects;
using API.Models.Suppliers;

namespace API.Services.Shared;

public interface ISupplierService
{
    public Task<Supplier> CreateSupplier(SupplierDto supplierDto);
    public Task<List<Supplier>> GetAllSuppliers();
}