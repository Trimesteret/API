using API.DataTransferObjects;
using API.Models.Items;

namespace API.Models.Suppliers;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<SupplierItemRelation>? Items { get; set; }
    /**
     * Empty constructor for EF core
     */
    public Supplier()
    {

    }

    public Supplier(string name)
    {
        this.Name = name;
    }
    public Supplier(string name, List<SupplierItemRelation> items)
    {
        this.Name = name;
        this.Items = items;
    }

    public void ChangeSupplierProperties(string name, List<SupplierItemRelation>? items)
    {
        this.Name = name;
        this.Items = items;
    }
}
