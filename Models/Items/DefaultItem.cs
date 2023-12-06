using API.Enums;

namespace API.Models.Items;

public class DefaultItem: Item
{
    /**
     * Parameterless constructor for EF Core
     */
    public DefaultItem()
    {

    }

    public DefaultItem(string name, string ean, int quantity, double price, string description, ItemType itemType)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.Description = description;
        this.ItemType = itemType;
    }

    public void ChangeDefaultItemProperties(string name, int supplierId, string ean, int quantity, double price, string description, ItemType itemType)
    {
        Name = name;
        SupplierId = supplierId;
        Ean = ean;
        Quantity = quantity;
        Price = price;
        Description = description;
        ItemType = itemType;
    }
}
