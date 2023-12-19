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
    public int ReservedQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public int? Year { get; set; }
    public double? Volume { get; set; }
    public double? AlcoholPercentage { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? GrapeSort { get; set; }
    public string? Winery { get; set; }
    public string? TastingNotes { get; set; }
    public List<int>? SuitableForEnumIds { get; set; }
    public ItemType ItemType { get; set; }
    public CustomEnum? WineTypeEnum { get; set; }
    public CustomEnum? LiquorTypeEnum { get; set; }
}
