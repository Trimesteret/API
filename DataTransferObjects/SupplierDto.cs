using API.Models.Items;

namespace API.DataTransferObjects;

public class SupplierDto
{
    public string Name { get; set; }
    public List<Item>? Items { get; set; }
}