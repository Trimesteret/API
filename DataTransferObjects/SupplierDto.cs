using API.Models.Items;

namespace API.DataTransferObjects;

public class SupplierDto
{
    public Int32 Id { get; set; }
    public string Name { get; set; }
    public List<Item>? Items { get; set; }
}