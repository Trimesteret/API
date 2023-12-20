using API.DataTransferObjects;
using API.Models.Orders;

namespace API.Services;

public interface IOrderService
{
    public Task<List<InboundOrderDto>> GetAllInboundOrders();

    public Task<List<PurchaseOrder>> GetAllPurchaseOrders();

    public Task<List<PurchaseOrder>> GetCurrentUserPurchaseOrders();

    public Task<InboundOrderDto> GetInboundOrderById(int id);

    public Task<InboundOrderDto> EditInboundOrder(InboundOrderDto inboundOrderDto);

    public Task<PurchaseOrderDto> EditPurchaseOrder(PurchaseOrderDto purchaseOrder);

    public Task<PurchaseOrderDto> GetPurchaseOrderById(int id);

    public Task<PurchaseOrderDto> CreatePurchaseOrder(PurchaseOrderDto purchaseOrder);

    public Task<InboundOrderDto> CreateInboundOrder(InboundOrderDto inboundOrder);

    public Task DeleteOrder(int id);
}
