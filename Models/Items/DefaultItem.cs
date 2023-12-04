namespace API.Models.Items;

public class DefaultItem: Item
{
    /**
     * Parameterless constructor for EF Core
     */
    public DefaultItem()
    {

    }

    public DefaultItem(string name, string ean, int quantity, float price, string imageUrl)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.ImageUrl = imageUrl;
    }
}
