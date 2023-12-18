using API.DataTransferObjects;
using API.Enums;
using API.Models.Items;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Tests;

public class ItemTest
{

    [Fact]
    public void Test1()
    {
        // Arrange
        var liquor = new Liquor();

        // Assert
        Assert.NotNull(liquor);
    }

    [Fact]
    public async void PassCreateLiquorTest()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var itemService = new ItemService(context, mapper);

        var customEnum1 = new CustomEnum { Key = "ltest1", Value = "lTest1", EnumType = EnumType.LiqourType};
        context.CustomEnums.Add(customEnum1);
        await context.SaveChangesAsync();

        var itemDto1 = new ItemDto { ItemType = ItemType.Liquor, Name = "Test1", Ean = "123456789", Quantity = 10, ReservedQuantity = 0, ImageUrl = "test",
            Price = 10, Description = "test", Year = 1999, Volume = 0.7, AlcoholPercentage = 40, LiquorTypeEnum = customEnum1 };

        var createdItemDto = await itemService.CreateItem(itemDto1);
        Assert.NotNull(createdItemDto);

        var createdLiquor = await context.Liquors.FirstOrDefaultAsync(liq => liq.Id == createdItemDto.Id);
        Assert.NotNull(createdLiquor);

        Assert.Equal(createdItemDto.Id, createdLiquor.Id);
        context.CustomEnums.Remove(customEnum1);
        context.Liquors.Remove(createdLiquor);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async void PassCreateWineTest()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var itemService = new ItemService(context, mapper);

        var customEnum1 = new CustomEnum { Key = "ltest1", Value = "lTest1", EnumType = EnumType.WineType};
        context.CustomEnums.Add(customEnum1);
        await context.SaveChangesAsync();

        var itemDto1 = new ItemDto { ItemType = ItemType.Wine, Name = "Test1", Ean = "123456789", Quantity = 10, ReservedQuantity = 0, ImageUrl = "test",
            Price = 10, Description = "test", Year = 1999, Volume = 0.7, AlcoholPercentage = 40, WineTypeEnum = customEnum1 };

        var createdItemDto = await itemService.CreateItem(itemDto1);
        Assert.NotNull(createdItemDto);

        var createdWine = await context.Wines.FirstOrDefaultAsync(wine => wine.Id == createdItemDto.Id);
        Assert.NotNull(createdWine);

        Assert.Equal(createdItemDto.Id, createdWine.Id);
        context.CustomEnums.Remove(customEnum1);
        context.Wines.Remove(createdWine);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async void PassCreateDefaultItemTest()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var itemService = new ItemService(context, mapper);

        await context.SaveChangesAsync();

        var itemDto1 = new ItemDto { ItemType = ItemType.DefaultItem, Name = "Test1", Ean = "123456789", Quantity = 10, ReservedQuantity = 0, ImageUrl = "test",
            Price = 10, Description = "test", Year = 1999, Volume = 0.7, AlcoholPercentage = 40 };

        var createdItemDto = await itemService.CreateItem(itemDto1);
        Assert.NotNull(createdItemDto);

        var createdItem = await context.DefaultItems.FirstOrDefaultAsync(item => item.Id == createdItemDto.Id);
        Assert.NotNull(createdItem);

        Assert.Equal(createdItemDto.Id, createdItem.Id);
        context.DefaultItems.Remove(createdItem);
        await context.SaveChangesAsync();
    }
}