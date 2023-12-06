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

    public DefaultItem(string name, string ean, int quantity, double price, string description)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.Description = description;
        this.ItemType = ItemType.DefaultItem;
    }

    public void ChangeDefaultItemProperties(string name, string ean, int quantity, double price, string description)
    {
        Name = name;
        Ean = ean;
        Quantity = quantity;
        Price = price;
        Description = description;
    }
}
