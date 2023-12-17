using API.DataTransferObjects;
using API.Enums;

namespace API.Models.Items;

public class DefaultItem: Item
{
    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public DefaultItem()
    {

    }

    public DefaultItem(ItemDto itemDto)
    {
        this.Name = itemDto.Name;
        this.Ean = itemDto.Ean;
        this.Quantity = itemDto.Quantity;
        this.ReservedQuantity = itemDto.ReservedQuantity;
        this.ImageUrl = itemDto.ImageUrl;
        this.Price = itemDto.Price;
        this.Description = itemDto.Description;
        this.ItemType = ItemType.DefaultItem;
    }

    public void ChangeDefaultItemProperties(ItemDto itemDto)
    {
        this.Name = itemDto.Name;
        this.Ean = itemDto.Ean;
        this.Quantity = itemDto.Quantity;
        this.ReservedQuantity = itemDto.ReservedQuantity;
        this.ImageUrl = itemDto.ImageUrl;
        this.Price = itemDto.Price;
        this.Description = itemDto.Description;
    }
}
