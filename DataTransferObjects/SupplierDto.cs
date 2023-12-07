using API.Models.Items;
using API.Models.Suppliers;

namespace API.DataTransferObjects;

public class SupplierDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public List<ItemRelation>? Items { get; set; }
}