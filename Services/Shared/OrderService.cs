using API.DataTransferObjects;
using API.Models;
using API.Models.Authentication;
using API.Models.Orders;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Services.Shared;

public class OrderService : IOrderService
{
    private readonly SharedContext _sharedContext;
    private readonly IMapper _mapper;
    private readonly IAuthService _authorizationService;

    public OrderService(SharedContext dbSharedContext, IMapper mapper, IAuthService authorizationService)
    {
        _sharedContext = dbSharedContext;
        _mapper = mapper;
        _authorizationService = authorizationService;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public async Task<List<Order>> GetAllOrders()
    {
        var orders = await _sharedContext.Order.ToListAsync();
        return orders;
    }

    /// <summary>
    /// Get inbound order by id
    /// </summary>
    /// <param name="id">Id of the inbound order</param>
    /// <returns></returns>
    /// <exception cref="Exception">If the inbound order is not found</exception>
    public async Task<InboundOrderDto> GetInboundOrderById(int id)
    {
        var inboundOrder = await _sharedContext.InboundOrders.FirstOrDefaultAsync(io => io.Id == id);

        if (inboundOrder == null)
        {
            throw new Exception("Inbound order not found");
        }

        var inboundOrderDto = _mapper.Map<InboundOrderDto>(inboundOrder);
        inboundOrderDto.OrderLines = await inboundOrder.GetOrderLines(_sharedContext);

        return inboundOrderDto;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="inboundOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<InboundOrderDto> EditInboundOrder(InboundOrderDto inboundOrder)
    {
        var inboundOrderToEdit = await _sharedContext.InboundOrders.FirstOrDefaultAsync(io => io.Id == inboundOrder.Id);

        if (inboundOrderToEdit == null)
        {
            throw new Exception("Inbound order not found");
        }

        await inboundOrderToEdit.SetOrderLines(_sharedContext, inboundOrder.OrderLines);

        await _sharedContext.SaveChangesAsync();

        var inboundOrderDto = _mapper.Map<InboundOrderDto>(inboundOrderToEdit);
        inboundOrderDto.OrderLines = await inboundOrderToEdit.GetOrderLines(_sharedContext);
        return inboundOrderDto;
    }

    /// <summary>
    /// Edits purchase order with the given purchase order
    /// </summary>
    /// <param name="purchaseOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<PurchaseOrderDto> EditPurchaseOrder(PurchaseOrderDto purchaseOrder)
    {
        var purchaseOrderToEdit = await _sharedContext.PurchaseOrders.FirstOrDefaultAsync(io => io.Id == purchaseOrder.Id);

        if (purchaseOrderToEdit == null)
        {
            throw new Exception("Purchase order not found");
        }

        await purchaseOrderToEdit.SetOrderLines(_sharedContext, purchaseOrder.OrderLines);

        await _sharedContext.SaveChangesAsync();

        var purchaseOrderDto = _mapper.Map<PurchaseOrderDto>(purchaseOrderToEdit);
        purchaseOrderDto.OrderLines = await purchaseOrderToEdit.GetOrderLines(_sharedContext);
        return purchaseOrderDto;
    }

    /// <summary>
    /// Get purchase order by id
    /// </summary>
    /// <param name="id">The id of the purchase order</param>
    /// <returns></returns>
    /// <exception cref="Exception">Throws if he purchaseOrder is not found</exception>
    public async Task<PurchaseOrderDto> GetPurchaseOrderById(int id)
    {
        var purchaseOrder = await _sharedContext.PurchaseOrders.FirstOrDefaultAsync(po => po.Id == id);

        if(purchaseOrder == null)
        {
            throw new Exception("Purchase order not found");
        }

        var purchaseOrderDto = _mapper.Map<PurchaseOrderDto>(purchaseOrder);

        purchaseOrderDto.OrderLines = await purchaseOrder.GetOrderLines(_sharedContext);

        return purchaseOrderDto;
    }

    /// <summary>
    /// Creates a new purchase order
    /// </summary>
    /// <param name="purchaseOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<PurchaseOrderDto> CreatePurchaseOrder(PurchaseOrderDto purchaseOrder)
    {
        var existingPurchaseOrder = await _sharedContext.PurchaseOrders.FirstOrDefaultAsync(po => po.Id == purchaseOrder.Id);

        if(existingPurchaseOrder != null)
        {
            throw new Exception("Purchase order already exists");
        }

        var purchaseOrderToCreate = new PurchaseOrder(purchaseOrder.OrderDate, purchaseOrder.DeliveryDate, purchaseOrder.DeliveryAddress, purchaseOrder.PurchaseOrderState);

        await purchaseOrderToCreate.SetOrderLines(_sharedContext, purchaseOrder.OrderLines);

        var activeUser = await this._authorizationService.GetActiveUser() as Customer;

        if(activeUser != null)
        {
            purchaseOrderToCreate.SetCustomer(activeUser);
        }

        _sharedContext.PurchaseOrders.Add(purchaseOrderToCreate);
        await _sharedContext.SaveChangesAsync();

        var purchaseOrderDto = _mapper.Map<PurchaseOrderDto>(purchaseOrderToCreate);
        purchaseOrderDto.OrderLines = await purchaseOrderToCreate.GetOrderLines(_sharedContext);
        return purchaseOrderDto;
    }

    public async Task<InboundOrderDto> CreateInboundOrder(InboundOrderDto inboundOrder)
    {
        var existingInboundOrder = await _sharedContext.InboundOrders.FirstOrDefaultAsync(po => po.Id == inboundOrder.Id);

        if(existingInboundOrder != null)
        {
            throw new Exception("Inbound order already exists");
        }

        var inboundOrderToCreate = new InboundOrder(inboundOrder.OrderDate, inboundOrder.DeliveryDate, inboundOrder.InboundOrderState);

        await inboundOrderToCreate.SetOrderLines(_sharedContext, inboundOrder.OrderLines);

        _sharedContext.InboundOrders.Add(inboundOrderToCreate);
        await _sharedContext.SaveChangesAsync();

        var purchaseOrderDto = _mapper.Map<InboundOrderDto>(inboundOrderToCreate);
        purchaseOrderDto.OrderLines = await inboundOrderToCreate.GetOrderLines(_sharedContext);
        return purchaseOrderDto;
    }
}
