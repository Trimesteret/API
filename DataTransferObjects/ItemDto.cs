using API.Enums;

namespace API.DataTransferObjects;

public class ItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Ean { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public int? Year { get; set; }
    public double? Volume { get; set; }
    public double? AlcoholPercentage { get; set; }
    public string Country { get; set; } = null;
    public string Region { get; set; } = null;
    public string GrapeSort { get; set; } = null;
    public string Winery { get; set; } = null;
    public string TastingNotes { get; set; } = null;
    public List<int>? SuitableForEnumIds { get; set; }
    public List<CustomEnum>? SuitableForEnums { get; set; }
    public WineType? WineType { get; set; }
    public ItemType ItemType { get; set; }
}
