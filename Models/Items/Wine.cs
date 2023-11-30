using System.Transactions;
using API.Enums;

namespace API.Models.Items;

public class Wine : Item
{
    public int? Year { get; protected set; }
    public double? Volume { get; protected set; }
    public double? AlcoholPercentage { get; protected set; }
    public string Country { get; protected set; }
    public string Region { get; protected set; }
    public string GrapeSort { get; protected set; }
    public string Winery { get; protected set; }
    public string TastingNotes { get; protected set; }
    public string SuitableFor { get; protected set; }
    public string ServingTemperature { get; protected set; }
    public WineType? WineType { get; protected set; }

    public Wine(){}

    public Wine(string name, string ean, int quantity, double price, string description, ItemType itemType, WineType? wineType)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.Description = description;
        this.WineType = wineType;
        this.ItemType = itemType;
    }
}
