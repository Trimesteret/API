namespace API.Models.Items;

public class Wine : Item
{
    public int? Year { get; protected set; }
    public double? Volume { get; protected set; }
    public double? AlcoholPercentage { get; protected set; }
    public string? Country { get; protected set; }
    public string? Region { get; protected set; }
    public string? GrapeSort { get; protected set; }
    public string? Winery { get; protected set; }
    // public List<string>? TastingNotes { get; protected set; }
    // public List<string>? SuitableFor { get; protected set; }
    // public List<string>? WineType { get; protected set; }

    public Wine()
    {
        
    }
    
    public Wine(string name, string ean, int quantity, float price, string imageUrl, DateTime expirationDate, int? year, double? volume, double? alcoholPercentage, string? country, string? region, string? grapeSort, string? winery, List<string>? tastingNotes, List<string>? suitableFor, List<string>? wineType)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.ImageUrl = imageUrl;
        this.ExpirationDate = expirationDate;
        this.Year = year;
        this.Volume = volume;
        this.AlcoholPercentage = alcoholPercentage;
        this.Country = country;
        this.Region = region;
        this.GrapeSort = grapeSort;
        this.Winery = winery;
        // this.TastingNotes = tastingNotes;
        // this.SuitableFor = suitableFor;
        // this.WineType = wineType;
    }
}
