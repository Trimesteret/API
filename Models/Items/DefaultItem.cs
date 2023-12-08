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

    public DefaultItem(string name, string ean, int quantity, double price, string description, string imageUrl)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.ImageUrl = imageUrl;
        this.Price = price;
        this.Description = description;
        this.ItemType = ItemType.DefaultItem;
    }

    public void ChangeDefaultItemProperties(string name, string ean, int quantity, double price, string description, string imageUrl)
    {
        Name = name;
        Ean = ean;
        Quantity = quantity;
        ImageUrl = imageUrl;
        Price = price;
        Description = description;
    }
}
