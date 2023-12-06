using API.Models.Items;

namespace API.DataTransferObjects;

public class SupplierDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public List<ItemDto>? Items { get; set; }
}