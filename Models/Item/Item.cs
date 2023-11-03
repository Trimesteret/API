namespace API.Models;

public class Item
{
    private int Id;
    private string Name;
    private string? BarCode;
    private int Quantity;
    private float Price;
    private DateTime ExpirationDate;


    public static List<Item> Items = new List<Item>();



    public int getId()
    {
        return this.Id;
    }

    public static List<Item> GetItems(DBContext context)
    {
        return context.Items.ToList();
    }
}


