using System.Runtime.InteropServices.JavaScript;
using API.Enums;

namespace API.DataTransferObjects;

public class ItemDto
{
    public Int32 Id { get; set; }
    public string ItemName { get; set; }
    public string Ean { get; set; }
    public string ItemDescription { get; set; }
    public float Price { get; set; }
    public int ItemQuantity { get; set; }
    public int? Year { get; set; }
    public double? Volume { get; set; }
    public double? AlcoholPercentage { get; set; }
    public string Country { get; set; } = null;
    public string Grapesort { get; set; } = null;
    public string Suitables { get; set; } = null;
    public WineType? WineType { get; set; }
    public ItemType ItemType { get; set; }
    public string ImageUrl { get; set; } = null;
}
