using API.Enums;
using API.Services;

namespace API.Tests;

[Collection("Sequential")]
public class EditItemTest
{
    /// <summary>
    /// Pass edit liqour
    /// </summary>
    [Fact]
    public async void PassEditLiquor()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var itemService = new ItemService(context, mapper);

        var originalItemDto = await SharedTesting.GetRandomItemDto(context, mapper, ItemType.Liquor, false);

        var createdItemDto = await itemService.CreateItem(originalItemDto);
        Assert.NotNull(createdItemDto);

        var editItemDto = await SharedTesting.GetRandomItemDto(context, mapper, ItemType.Liquor, false);
        editItemDto.Id = createdItemDto.Id;

        var editedItemDto = await itemService.EditItem(editItemDto);
        Assert.NotNull(editedItemDto);

        Assert.Equal(editItemDto.Id, editedItemDto.Id);
        Assert.Equal(editItemDto.ItemType, editedItemDto.ItemType);
        Assert.Null(editedItemDto.SuitableForEnumIds);
        Assert.Equal(editItemDto.AlcoholPercentage, editedItemDto.AlcoholPercentage);
        Assert.Null(editedItemDto.Country);
        Assert.Equal(editItemDto.Description, editedItemDto.Description);
        Assert.Equal(editedItemDto.Ean, editItemDto.Ean);
        Assert.Null(editedItemDto.GrapeSort);
        Assert.Equal(editedItemDto.ImageUrl, editItemDto.ImageUrl);
        Assert.Equal(editedItemDto.LiquorTypeEnum, editItemDto.LiquorTypeEnum);
        Assert.Equal(editedItemDto.Name, editItemDto.Name);
        Assert.Equal(editedItemDto.Price, editItemDto.Price);
        Assert.Equal(editedItemDto.Quantity, editItemDto.Quantity);
        Assert.Null(editedItemDto.Region);
        Assert.Equal(editedItemDto.ReservedQuantity, editedItemDto.ReservedQuantity);
        Assert.Null(editedItemDto.WineTypeEnum);
        Assert.Null(editedItemDto.Winery);
        Assert.Null(editedItemDto.Year);
        await context.Database.EnsureDeletedAsync();
    }

    /// <summary>
    /// Pass edit wine
    /// </summary>
    [Fact]
    public async void PassEditWine()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var itemService = new ItemService(context, mapper);

        var originalItemDto = await SharedTesting.GetRandomItemDto(context, mapper, ItemType.Wine, false);
        var createdItemDto = await itemService.CreateItem(originalItemDto);
        Assert.NotNull(createdItemDto);

        var editItemDto = await SharedTesting.GetRandomItemDto(context, mapper, ItemType.Wine, false);
        editItemDto.Id = createdItemDto.Id;

        var editedItemDto = await itemService.EditItem(editItemDto);
        Assert.NotNull(editedItemDto);

        Assert.Equal(editItemDto.Id, editedItemDto.Id);
        Assert.Equal(editItemDto.ItemType, editedItemDto.ItemType);
        Assert.True(editItemDto.SuitableForEnumIds.All(id => editedItemDto.SuitableForEnumIds.Contains(id)));
        Assert.Equal(editItemDto.AlcoholPercentage, editedItemDto.AlcoholPercentage);
        Assert.Equal(editItemDto.Country, editedItemDto.Country);
        Assert.Equal(editItemDto.Description, editedItemDto.Description);
        Assert.Equal(editedItemDto.Ean, editItemDto.Ean);
        Assert.Equal(editItemDto.GrapeSort, editedItemDto.GrapeSort);
        Assert.Equal(editedItemDto.ImageUrl, editItemDto.ImageUrl);
        Assert.Null(editedItemDto.LiquorTypeEnum);
        Assert.Equal(editedItemDto.Name, editItemDto.Name);
        Assert.Equal(editedItemDto.Price, editItemDto.Price);
        Assert.Equal(editedItemDto.Quantity, editItemDto.Quantity);
        Assert.Equal(editedItemDto.Region, editItemDto.Region);
        Assert.Equal(editedItemDto.ReservedQuantity, editedItemDto.ReservedQuantity);
        Assert.Equal(editedItemDto.WineTypeEnum, editItemDto.WineTypeEnum);
        Assert.Equal(editedItemDto.Winery, editItemDto.Winery);
        Assert.Equal(editedItemDto.Year, editItemDto.Year);
        await context.Database.EnsureDeletedAsync();
    }

    /// <summary>
    /// Pass edit default item
    /// </summary>
    [Fact]
    public async void PassEditDefaultItem()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var itemService = new ItemService(context, mapper);

        var originalItemDto = await SharedTesting.GetRandomItemDto(context, mapper, ItemType.DefaultItem, false);

        var createdItemDto = await itemService.CreateItem(originalItemDto);
        Assert.NotNull(createdItemDto);

        var editItemDto = await SharedTesting.GetRandomItemDto(context, mapper, ItemType.DefaultItem, false);
        editItemDto.Id = createdItemDto.Id;

        var editedItemDto = await itemService.EditItem(editItemDto);
        Assert.NotNull(editedItemDto);

        Assert.Equal(editItemDto.Id, editedItemDto.Id);
        Assert.Equal(editItemDto.ItemType, editedItemDto.ItemType);
        Assert.Null(editedItemDto.SuitableForEnumIds);
        Assert.Null(editedItemDto.AlcoholPercentage);
        Assert.Null(editedItemDto.Country);
        Assert.Equal(editItemDto.Description, editedItemDto.Description);
        Assert.Equal(editedItemDto.Ean, editItemDto.Ean);
        Assert.Null(editedItemDto.GrapeSort);
        Assert.Equal(editedItemDto.ImageUrl, editItemDto.ImageUrl);
        Assert.Null(editedItemDto.LiquorTypeEnum);
        Assert.Equal(editedItemDto.Name, editItemDto.Name);
        Assert.Equal(editedItemDto.Price, editItemDto.Price);
        Assert.Equal(editedItemDto.Quantity, editItemDto.Quantity);
        Assert.Null(editedItemDto.Region);
        Assert.Equal(editedItemDto.ReservedQuantity, editedItemDto.ReservedQuantity);
        Assert.Null(editedItemDto.WineTypeEnum);
        Assert.Null(editedItemDto.Winery);
        Assert.Null(editedItemDto.Year);
        await context.Database.EnsureDeletedAsync();
    }

    /// <summary>
    /// Fail to edit item with null id
    /// </summary>
    [Fact]
    public async void FailEditNullItem()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var itemService = new ItemService(context, mapper);

        var nonExistingDto = await SharedTesting.GetRandomItemDto(context, mapper, null, false);

        var exception = await Assert.ThrowsAsync<Exception>(async () => await itemService.EditItem(nonExistingDto));
        Assert.Equal("Could not find item with id: " + nonExistingDto.Id, exception.Message);
        await context.Database.EnsureDeletedAsync();
    }
}
