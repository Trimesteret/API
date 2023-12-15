using API.DataTransferObjects;
using API.Models;
using API.Models.Authentication;
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

        var orderLines = _mapper.Map<List<OrderLine>>(inboundOrder.OrderLines);

        inboundOrderToEdit.SetOrderLines(orderLines);

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

        var orderLines = _mapper.Map<List<OrderLine>>(purchaseOrder.OrderLines);

        purchaseOrderToEdit.SetOrderLines(orderLines);

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
        var purchaseOrder = await _sharedContext.PurchaseOrders.FirstOrDefaultAsync(po => po.Id == id);

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

        var purchaseOrderToCreate = new PurchaseOrder(purchaseOrder.OrderDate, purchaseOrder.DeliveryDate, purchaseOrder.Address, purchaseOrder.PurchaseOrderState);

        var orderLines = _mapper.Map<List<OrderLine>>(purchaseOrder.OrderLines);

        purchaseOrderToCreate.SetOrderLines(orderLines);

        Console.WriteLine(purchaseOrder);
        var user = await _sharedContext.Users.FirstOrDefaultAsync(user => user.Email == purchaseOrder.OrderCustomer.Email);

        var customer = user as Customer;

        if (user != null && customer == null)
        {
            throw new Exception("User is not a customer, and can therefore not create a purchase order");
        }

        if (user == null)
        {
            var customerDto = purchaseOrder.OrderCustomer;
            customer = new Customer(customerDto.FirstName, customerDto.LastName,  customerDto.Phone, customerDto.Email, null, null);
            _sharedContext.Customers.Add(customer);
            await _sharedContext.SaveChangesAsync();
        }

        purchaseOrderToCreate.SetCustomer(customer);

        _sharedContext.PurchaseOrders.Add(purchaseOrderToCreate);
        await _sharedContext.SaveChangesAsync();

        var purchaseOrderDto = _mapper.Map<PurchaseOrderDto>(purchaseOrderToCreate);
        return purchaseOrderDto;
    }

    /// <summary>
    /// Creates a new inbound order
    /// </summary>
    /// <param name="inboundOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<InboundOrderDto> CreateInboundOrder(InboundOrderDto inboundOrder)
    {
        var existingInboundOrder = await _sharedContext.InboundOrders.FirstOrDefaultAsync(po => po.Id == inboundOrder.Id);

        if(existingInboundOrder != null)
        {
            throw new Exception("Inbound order already exists");
        }

        var inboundOrderToCreate = new InboundOrder(inboundOrder.OrderDate, inboundOrder.DeliveryDate, inboundOrder.InboundOrderState);

        var orderLines = _mapper.Map<List<OrderLine>>(inboundOrder.OrderLines);

        inboundOrderToCreate.SetOrderLines(orderLines);

        _sharedContext.InboundOrders.Add(inboundOrderToCreate);
        await _sharedContext.SaveChangesAsync();

        var purchaseOrderDto = _mapper.Map<InboundOrderDto>(inboundOrderToCreate);
        return purchaseOrderDto;
    }
}