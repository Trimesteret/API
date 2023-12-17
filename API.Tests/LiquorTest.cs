using API.DataTransferObjects;
using API.Enums;
using API.Models.Items;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Tests;

public class LiquorTest
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
    public async void CreateLiquorTest()
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
    }
}