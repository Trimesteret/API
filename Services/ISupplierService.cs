using API.DataTransferObjects;
using API.Models.Items;
using API.Models.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace API.Services;

public interface ISupplierService
{
    public Task<SupplierDto> CreateSupplier(SupplierDto supplierDto);

    public Task<List<Supplier>> GetAllSuppliers();

    public Task<SupplierDto> GetSupplierById(int id);

    public Task<SupplierDto> EditSupplier(SupplierDto supplier);

    public Task DeleteSupplier(int id);
}
