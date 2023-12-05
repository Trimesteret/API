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
    public List<SuitableFor> SuitableFor { get; protected set; }
    public WineType? WineType { get; protected set; }

    /**
     * Parameterless constructor for EF Core
     */
    public Wine()
    {

    }

    public Wine(string name, string ean, int quantity, double price, string description,
                WineType? wineType, int? year, double? volume,
                double? alcoholPercentage, string country, string region, string grapeSort,
                string winery, string tastingNotes, List<SuitableFor> suitableFor)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.Description = description;
        this.WineType = wineType;
        this.ItemType = ItemType.Wine;
        this.Year = year;
        this.Volume = volume;
        this.AlcoholPercentage = alcoholPercentage;
        this.Country = country;
        this.Region = region;
        this.GrapeSort = grapeSort;
        this.Winery = winery;
        this.TastingNotes = tastingNotes;
        this.SuitableFor = suitableFor;
    }

    public void ChangeWineProperties(string name, string ean, int quantity, double price, string description,
        WineType? wineType, int? year, double? volume, double? alcoholPercentage, string country, string region,
        string grapeSort, string winery, string tastingNotes, List<SuitableFor> suitableFor)
    {
        Name = name;
        Ean = ean;
        Quantity = quantity;
        Price = price;
        Description = description;
        WineType = wineType;
        Year = year;
        Volume = volume;
        AlcoholPercentage = alcoholPercentage;
        Country = country;
        Region = region;
        GrapeSort = grapeSort;
        Winery = winery;
        TastingNotes = tastingNotes;
        SuitableFor = suitableFor;
    }
}
