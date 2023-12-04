using API.Enums;

namespace API.Models.Items;

public class DefaultItem: Item
{
    public DefaultItem(){}

    public DefaultItem(string name, string ean, int quantity, double price, string description, ItemType itemType)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.Description = description;
        this.ItemType = itemType;
    }
}
