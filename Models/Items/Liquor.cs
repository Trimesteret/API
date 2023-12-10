using API.Enums;

namespace API.Models.Items;

public class Liquor: Item
{
    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public Liquor()
    {

    }


    public Liquor(string name, string ean, int quantity, double price, string description, string imageUrl)
    {
        this.Name = name;
        this.Ean = ean;
        this.ImageUrl = imageUrl;
        this.Quantity = quantity;
        this.Price = price;
        this.Description = description;
        this.ItemType = ItemType.Liquor;
    }

    public void ChangeLiquorProperties(string name, string ean, int quantity, double price, string description, string imageUrl)
    {
        Name = name;
        Ean = ean;
        Quantity = quantity;
        ImageUrl = imageUrl;
        Price = price;
        Description = description;
    }
}
