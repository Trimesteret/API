using API.DataTransferObjects;
using API.Enums;
using API.Services;

namespace API.Tests;

public class EditItemTest
{
    [Fact]
    public async void PassEditLiquor()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var itemService = new ItemService(context, mapper);

        var customEnum1 = new CustomEnum { Key = "ltest1", Value = "lTest1", EnumType = EnumType.LiqourType};
        var customEnum2 = new CustomEnum { Key = "ltest2", Value = "lTest2", EnumType = EnumType.LiqourType};
        context.CustomEnums.Add(customEnum1);
        context.CustomEnums.Add(customEnum2);
        await context.SaveChangesAsync();

        var originalItemDto = new ItemDto { ItemType = ItemType.Liquor, Name = "Test1", Ean = "123456789", Quantity = 10, ReservedQuantity = 0, ImageUrl = "test",
            Price = 10, Description = "test", Year = 1999, Volume = 0.7, AlcoholPercentage = 40, LiquorTypeEnum = customEnum1 };

        var createdItemDto = await itemService.CreateItem(originalItemDto);
        Assert.NotNull(createdItemDto);

        var editItemDto = new ItemDto { Id = createdItemDto.Id, ItemType = ItemType.Liquor, Name = "Testen", Ean = "132132345", Quantity = 14, ReservedQuantity = 3, ImageUrl = "test",
            Price = 109, Description = "testen er lykkedes", Year = 2031, Volume = 1.6, AlcoholPercentage = 26, LiquorTypeEnum = customEnum2 };

        var editedItemDto = await itemService.EditItem(editItemDto);
        Assert.NotNull(editedItemDto);

        Assert.Equal(editedItemDto.Id, editItemDto.Id);
        Assert.Equal(editedItemDto.Name, editItemDto.Name);
        Assert.Equal(editedItemDto.Ean, editItemDto.Ean);
        Assert.Equal(editedItemDto.Quantity, editItemDto.Quantity);
        Assert.Equal(editedItemDto.ReservedQuantity, editItemDto.ReservedQuantity);
        Assert.Equal(editedItemDto.ImageUrl, editItemDto.ImageUrl);
        Assert.Equal(editedItemDto.Price, editItemDto.Price);
        Assert.Equal(editedItemDto.Description, editItemDto.Description);
        Assert.Equal(editedItemDto.Year, editItemDto.Year);
        Assert.Equal(editedItemDto.Volume, editItemDto.Volume);
        Assert.Equal(editedItemDto.AlcoholPercentage, editItemDto.AlcoholPercentage);
        Assert.Equal(editedItemDto.LiquorTypeEnum?.Id, editItemDto.LiquorTypeEnum.Id);
    }

    [Fact]
    public async void PassEditWine()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var itemService = new ItemService(context, mapper);

        var customEnum1 = new CustomEnum { Key = "wtest1", Value = "wTest1", EnumType = EnumType.WineType};
        var customEnum2 = new CustomEnum { Key = "wtest2", Value = "wTest2", EnumType = EnumType.WineType};
        var suitableFor1 = new CustomEnum { Key = "stest1", Value = "sTest1", EnumType = EnumType.SuitableFor};
        var suitableFor2 = new CustomEnum { Key = "stest2", Value = "sTest2", EnumType = EnumType.SuitableFor};
        context.CustomEnums.Add(customEnum1);
        context.CustomEnums.Add(customEnum2);
        await context.SaveChangesAsync();

        var originalItemDto = new ItemDto { ItemType = ItemType.Wine, Name = "Test1", Ean = "123456789", Quantity = 10, ReservedQuantity = 0, ImageUrl = "test",
            Price = 10, Description = "test", Year = 1999, Volume = 0.7, AlcoholPercentage = 40, WineTypeEnum = customEnum1 };

        var createdItemDto = await itemService.CreateItem(originalItemDto);
        Assert.NotNull(createdItemDto);

        var editItemDto = new ItemDto { Id = createdItemDto.Id, ItemType = ItemType.Wine, Name = "Testen", Ean = "132132345", Quantity = 14, ReservedQuantity = 3, ImageUrl = "test",
            Price = 109, Description = "testen er lykkedes", Year = 2031, Volume = 1.6, AlcoholPercentage = 26, WineTypeEnum = customEnum2 };

        var editedItemDto = await itemService.EditItem(editItemDto);
        Assert.NotNull(editedItemDto);

        Assert.Equal(editedItemDto.Id, editItemDto.Id);
        Assert.Equal(editedItemDto.Name, editItemDto.Name);
        Assert.Equal(editedItemDto.Ean, editItemDto.Ean);
        Assert.Equal(editedItemDto.Quantity, editItemDto.Quantity);
        Assert.Equal(editedItemDto.ReservedQuantity, editItemDto.ReservedQuantity);
        Assert.Equal(editedItemDto.ImageUrl, editItemDto.ImageUrl);
        Assert.Equal(editedItemDto.Price, editItemDto.Price);
        Assert.Equal(editedItemDto.Description, editItemDto.Description);
        Assert.Equal(editedItemDto.Year, editItemDto.Year);
        Assert.Equal(editedItemDto.Volume, editItemDto.Volume);
        Assert.Equal(editedItemDto.AlcoholPercentage, editItemDto.AlcoholPercentage);
        Assert.Equal(editedItemDto.WineTypeEnum?.Id, editItemDto.WineTypeEnum.Id);
    }
}
