using API.DataTransferObjects;
using API.Models;
using API.Models.Orders;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Services;

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
    /// Gets all purchase orders in the application
    /// </summary>
    /// <returns></returns>
    public async Task<List<PurchaseOrder>> GetAllPurchaseOrders()
    {
       return await _sharedContext.PurchaseOrders.ToListAsync();
    }

    /// <summary>
    /// Gets all purchase orders for the current user
    /// </summary>
    /// <returns></returns>
    public async Task<List<PurchaseOrder>> GetCurrentUserPurchaseOrders()
    {
        try
        {
            var activeCustomer = await this._authorizationService.GetActiveUserAsCustomer();

            return await activeCustomer.GetPurchaseOrders(_sharedContext);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<PurchaseOrder>();
        }
    }

    /// <summary>
    /// Gets all inbound orders in the application
    /// </summary>
    /// <returns></returns>
    public async Task<List<InboundOrder>> GetAllInboundOrders()
    {
        return await _sharedContext.InboundOrders.ToListAsync();
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

        await _sharedContext.SaveChangesAsync();

        var inboundOrderDto = _mapper.Map<InboundOrderDto>(inboundOrderToEdit);
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

        await _sharedContext.SaveChangesAsync();

        var purchaseOrderDto = _mapper.Map<PurchaseOrderDto>(purchaseOrderToEdit);
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
        var purchaseOrder = await _sharedContext.PurchaseOrders.Include(po => po.OrderLines)
            .ThenInclude(ol => ol.Item).FirstOrDefaultAsync(po => po.Id == id);

        if(purchaseOrder == null)
        {
            throw new Exception("Purchase order not found");
        }

        var purchaseOrderDto = _mapper.Map<PurchaseOrderDto>(purchaseOrder);

        return purchaseOrderDto;
    }

    public async Task<List<OrderLineDto>> GetPurchaseOrderOrderLines(int purchaseOrderId)
    {
        var purchaseOrder = await _sharedContext.PurchaseOrders.Include(order => order.OrderLines)
            .ThenInclude(orderLine => orderLine.Item).FirstOrDefaultAsync(po => po.Id == purchaseOrderId);

        if(purchaseOrder == null)
        {
            throw new Exception("Purchase order not found");
        }

        var orderLinesDtos = _mapper.Map<List<OrderLineDto>>(purchaseOrder.OrderLines);

        return orderLinesDtos;
    }

    /// <summary>
    /// Creates a new purchase order
    /// </summary>
    /// <param name="purchaseOrderDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<PurchaseOrderDto> CreatePurchaseOrder(PurchaseOrderDto purchaseOrderDto)
    {
        var existingPurchaseOrder = await _sharedContext.PurchaseOrders.FirstOrDefaultAsync(po => po.Id == purchaseOrderDto.Id);

        if(existingPurchaseOrder != null)
        {
            throw new Exception("Purchase order already exists");
        }

        var purchaseOrderToCreate = new PurchaseOrder(purchaseOrderDto);

        _sharedContext.PurchaseOrders.Add(purchaseOrderToCreate);
        await _sharedContext.SaveChangesAsync();

        return purchaseOrderDto;
    }

    /// <summary>
    /// Creates a new inbound order
    /// </summary>
    /// <param name="inboundOrderDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<InboundOrderDto> CreateInboundOrder(InboundOrderDto inboundOrderDto)
    {
        var existingInboundOrder = await _sharedContext.InboundOrders.FirstOrDefaultAsync(po => po.Id == inboundOrderDto.Id);

        if(existingInboundOrder != null)
        {
            throw new Exception("Inbound order already exists");
        }

        var inboundOrderToCreate = new InboundOrder(inboundOrderDto);

        _sharedContext.InboundOrders.Add(inboundOrderToCreate);
        await _sharedContext.SaveChangesAsync();

        return inboundOrderDto;
    }
}
