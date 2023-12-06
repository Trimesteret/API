using API.Enums;

namespace API.Models.Items;

public class Liquor: Item
{
    /**
     * Parameterless constructor for EF Core
     */
    public Liquor()
    {

    }


    public Liquor(string name, string ean, int quantity, double price, string description, ItemType itemType)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.Description = description;
        this.ItemType = itemType;
    }
    public void ChangeLiquorProperties(string name, string ean, int quantity, double price, string description, ItemType itemType)
    {
        Name = name;
        Ean = ean;
        Quantity = quantity;
        Price = price;
        Description = description;
        ItemType = itemType;
    }
}
