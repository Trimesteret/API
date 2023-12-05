using System.Runtime.InteropServices.JavaScript;
using API.Enums;

namespace API.DataTransferObjects;

public class ItemDto
{
    public Int32 Id { get; set; }
    public string ItemName { get; set; }
    public string Ean { get; set; }
    public string ItemDescription { get; set; }
    public double Price { get; set; }
    public int ItemQuantity { get; set; }
    public int? Year { get; set; }
    public double? Volume { get; set; }
    public double? AlcoholPercentage { get; set; }
    public string Country { get; set; } = null;
    public string Region { get; set; } = null;
    public string GrapeSort { get; set; } = null;
    public string Winery { get; set; } = null;
    public string TastingNotes { get; set; } = null;
    public List<SuitableFor> SuitableFor { get; set; } = null;
    public WineType? WineType { get; set; }
    public ItemType ItemType { get; set; }
}
