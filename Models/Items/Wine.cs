namespace API.Models.Items;

public class Wine : Item
{
    public Wine(string name, string ean, int quantity, float price, string imageUrl, DateTime expirationDate, string liquorType)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.ImageUrl = imageUrl;
        this.ExpirationDate = expirationDate;
    }
}
