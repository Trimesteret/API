using API.Models.Items;

namespace API.Models.Suppliers;

public class Supplier
{
    public int Id { get; set; }
    public Item[]? Items { get; set; }
}