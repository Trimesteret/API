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
    private readonly IItemService _itemService;

    public OrderService(SharedContext dbSharedContext, IMapper mapper, IAuthService authorizationService, IItemService itemService)
    {
        _sharedContext = dbSharedContext;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _itemService = itemService;
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
    public async Task<List<InboundOrderDto>> GetAllInboundOrders()
    {
        var inboundOrders = await _sharedContext.InboundOrders.Include(inbound => inbound.Supplier).ToListAsync();

        var inboundOrderDtos = _mapper.Map<List<InboundOrderDto>>(inboundOrders);
        return inboundOrderDtos;
    }

    /// <summary>
    /// Get inbound order by id
    /// </summary>
    /// <param name="id">Id of the inbound order</param>
    /// <returns></returns>
    /// <exception cref="Exception">If the inbound order is not found</exception>
    public async Task<InboundOrderDto> GetInboundOrderById(int id)
    {
        var inboundOrder = await _sharedContext.InboundOrders.Include(po => po.Supplier).Include(po => po.OrderLines)
            .FirstOrDefaultAsync(io => io.Id == id);

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
    /// <param name="inboundOrderDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<InboundOrderDto> EditInboundOrder(InboundOrderDto inboundOrderDto)
    {
        var inboundOrderToEdit = await _sharedContext.InboundOrders.FirstOrDefaultAsync(io => io.Id == inboundOrderDto.Id);

        if (inboundOrderToEdit == null)
        {
            throw new Exception("Inbound order not found");
        }

        await inboundOrderToEdit.ClearOrderLines(_sharedContext);
        inboundOrderToEdit.ChangeInboundOrderProperties(inboundOrderDto);
        await _sharedContext.SaveChangesAsync();

        var inboundOrderResponse = _mapper.Map<InboundOrderDto>(inboundOrderToEdit);
        inboundOrderResponse.Supplier = inboundOrderDto.Supplier;
        return inboundOrderResponse;
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
        purchaseOrderToEdit.ChangeOrderProperties(purchaseOrder);
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

        await _itemService.ReserveItems(purchaseOrderToCreate.OrderLines);
        await _sharedContext.SaveChangesAsync();
        var purchaseOrderResponse = _mapper.Map<PurchaseOrderDto>(purchaseOrderToCreate);
        
        return purchaseOrderResponse;
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

        var supplier = await _sharedContext.Suppliers.FirstOrDefaultAsync(supplier => supplier.Id == inboundOrderDto.Supplier.Id);

        if(supplier == null)
        {
            throw new Exception("Supplier not found with id: " + inboundOrderDto.Supplier.Id);
        }

        var inboundOrderToCreate = new InboundOrder(inboundOrderDto, supplier);

        _sharedContext.InboundOrders.Add(inboundOrderToCreate);
        await _sharedContext.SaveChangesAsync();

        var inboundOrderDtoToReturn = _mapper.Map<InboundOrderDto>(inboundOrderToCreate);

        return inboundOrderDtoToReturn;
    }

    /// <summary>
    /// Deletes an order by id
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public async Task DeleteOrder(int id)
    {
        var order = await _sharedContext.Orders.FirstOrDefaultAsync(order => order.Id == id);

        if (order == null)
        {
            throw new Exception("Order not found");
        }

        _sharedContext.Orders.Remove(order);
        await _sharedContext.SaveChangesAsync();
    }
}
