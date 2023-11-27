using System.Runtime.InteropServices.JavaScript;
using API.Enums;

namespace API.DataTransferObjects;

public class ItemDto
{
    public Int32 Id { get; set; }
    public string? ItemName { get; set; }
    public string? Ean { get; set; }
    public string? ItemDescription { get; set; }
    public double ItemPrice { get; set; }
    public int ItemQuantity { get; set; }
    public int? Year { get; set; }
    public double? Volume { get; set; }
    public double? AlcoholPercentage { get; set; }
    public string? Country { get; set; }
    public string? Grapesort { get; set; }
    public string? Suitables { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public WineType? WineType { get; set; }
    public ItemType? Type { get; set; }
    public string? ImageUrl { get; set; }
}
