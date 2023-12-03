using API.Models.Items;

namespace API.Models.Suppliers;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Item>? Items { get; set; }
    /**
     * Empty constructor for EF core
     */
    public Supplier()
    {
        
    }
    public Supplier(string name, List<Item> items)
    {
        this.Name = name;
        this.Items = items;
    }

}
