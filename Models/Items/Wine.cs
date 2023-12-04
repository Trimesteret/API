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

    /**
     * Parameterless constructor for EF Core
     */
    public Wine()
    {

    }

    public Wine(string name, string ean, int quantity, float price, string imageUrl, string description, float mass, int? year, double? volume, double? alcoholPercentage, string country, string region, string grapeSort, string winery, string tastingNotes, string suitableFor, string servingTemperature)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.Price = price;
        this.ImageUrl = imageUrl;
        this.Description = description;
        this.Mass = mass;
        this.Year = year;
        this.Volume = volume;
        this.AlcoholPercentage = alcoholPercentage;
        this.Country = country;
        this.Region = region;
        this.GrapeSort = grapeSort;
        this.Winery = winery;
        this.TastingNotes = tastingNotes;
        this.SuitableFor = suitableFor;
        this.ServingTemperature = servingTemperature;
    }
}
