using API.DataTransferObjects;
using API.Enums;
using API.Services;
using Microsoft.AspNetCore.Http;
namespace API.Tests;

[Collection("Sequential")]
public class CreatePurchaseOrderTest
{
    [Fact]
    public async void PassCreatePurchaseOrderTest()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();
        var httpProcessor = new HttpContextAccessor();
        var authService = new AuthService(httpProcessor, context);
        var itemService = new ItemService(context, mapper);
        var orderService = new OrderService(context, mapper, authService, itemService);

        var customEnum1 = new CustomEnum { Key = "ltest1", Value = "lTest1", EnumType = EnumType.WineType};
        context.CustomEnums.Add(customEnum1);
        
        await context.SaveChangesAsync();

        var testItemDto = new ItemDto 
        {
            Id = 1, 
            ItemType = ItemType.Wine, 
            Name = "Test1", 
            Ean = "123456789", 
            Quantity = 50, 
            ReservedQuantity = 0, 
            ImageUrl = "test",
            Price = 10, 
            Description = "test", 
            Year = 1999, 
            Volume = 0.7, 
            AlcoholPercentage = 40, 
            SuitableForEnumIds = new List<int> { 0, 1 }, 
            WineTypeEnum = customEnum1 
        };
       
        await itemService.CreateItem(testItemDto);
        
        var testOrderLines = new List<OrderLineDto>
        {
            new OrderLineDto{ItemId = 1,LinePrice = 20,ItemPrice = 5,ItemName = "Testvin1",Quantity = 4},
            new OrderLineDto{ItemId = 1,LinePrice = 60,ItemPrice = 10,ItemName = "Testvin2",Quantity = 6},
            new OrderLineDto{ItemId = 1,LinePrice = 100,ItemPrice = 20,ItemName = "Testvin3",Quantity = 5}
        };
        var testPurchaseOrder = new PurchaseOrderDto
        {
            TotalPrice = 20, 
            CustomerFirstName = "Bob", 
            CustomerLastName = "Testmand", 
            CustomerPhone = "12121212", 
            CustomerEmail = "bobtestmand@gmail.com", 
            AddressLine = "Testvej 51", 
            PostalCode = "9000", 
            City = "Aalborg", 
            Country = "Danmark", 
            OrderLines = testOrderLines, 
            PurchaseOrderState = PurchaseOrderState.Payed
        };

        var createdPurchaseOrder = await orderService.CreatePurchaseOrder(testPurchaseOrder);
        Assert.NotNull(createdPurchaseOrder);
        await context.Database.EnsureDeletedAsync();
        
    }
    [Fact]
    public async void FailCreatePurchaseOrderTestOrderAlreadyExists()
    {
         var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();
        var httpProcessor = new HttpContextAccessor();
        var authService = new AuthService(httpProcessor, context);
        var itemService = new ItemService(context, mapper);
        var orderService = new OrderService(context, mapper, authService, itemService);

        var customEnum1 = new CustomEnum { Key = "ltest1", Value = "lTest1", EnumType = EnumType.WineType};
        context.CustomEnums.Add(customEnum1);
        
        await context.SaveChangesAsync();

        var testItemDto = new ItemDto 
        {
            Id = 1, 
            ItemType = ItemType.Wine, 
            Name = "Test1", 
            Ean = "123456789", 
            Quantity = 50, 
            ReservedQuantity = 0, 
            ImageUrl = "test",
            Price = 10, 
            Description = "test", 
            Year = 1999, 
            Volume = 0.7, 
            AlcoholPercentage = 40, 
            SuitableForEnumIds = new List<int> { 0, 1 }, 
            WineTypeEnum = customEnum1 
        };
       
        await itemService.CreateItem(testItemDto);
        
        var testOrderLines = new List<OrderLineDto>
        {
            new OrderLineDto{ItemId = 1,LinePrice = 20,ItemPrice = 5,ItemName = "Testvin1",Quantity = 4},
            new OrderLineDto{ItemId = 1,LinePrice = 60,ItemPrice = 10,ItemName = "Testvin2",Quantity = 6},
            new OrderLineDto{ItemId = 1,LinePrice = 100,ItemPrice = 20,ItemName = "Testvin3",Quantity = 5}
        };
        var testPurchaseOrder = new PurchaseOrderDto
        {
            Id = 1,
            TotalPrice = 20, 
            CustomerFirstName = "Bob", 
            CustomerLastName = "Testmand", 
            CustomerPhone = "12121212", 
            CustomerEmail = "bobtestmand@gmail.com", 
            AddressLine = "Testvej 51", 
            PostalCode = "9000", 
            City = "Aalborg", 
            Country = "Danmark", 
            OrderLines = testOrderLines, 
            PurchaseOrderState = PurchaseOrderState.Payed
        };
        
        await orderService.CreatePurchaseOrder(testPurchaseOrder);
        await Assert.ThrowsAsync<Exception>(async () =>await orderService.CreatePurchaseOrder(testPurchaseOrder));
        await context.Database.EnsureDeletedAsync();
    }
}