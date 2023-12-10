using API.Enums;
using Microsoft.EntityFrameworkCore;

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
    public List<ItemEnumRelation>? SuitableFor { get; set; }
    public WineType? WineType { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public Wine()
    {

    }

    public Wine(string name, string ean, int quantity, string imageUrl, double price, string description,
                WineType? wineType, int? year, double? volume,
                double? alcoholPercentage, string country, string region, string grapeSort,
                string winery, string tastingNotes)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.ImageUrl = imageUrl;
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
    }

    /// <summary>
    /// Function for changing the properties of a wine
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ean"></param>
    /// <param name="quantity"></param>
    /// <param name="imageUrl"></param>
    /// <param name="price"></param>
    /// <param name="description"></param>
    /// <param name="wineType"></param>
    /// <param name="year"></param>
    /// <param name="volume"></param>
    /// <param name="alcoholPercentage"></param>
    /// <param name="country"></param>
    /// <param name="region"></param>
    /// <param name="grapeSort"></param>
    /// <param name="winery"></param>
    /// <param name="tastingNotes"></param>
    public void ChangeWineProperties(string name, string ean, int quantity, string imageUrl, double price, string description,
        WineType? wineType, int? year, double? volume, double? alcoholPercentage, string country, string region,
        string grapeSort, string winery, string tastingNotes)
    {
        this.Name = name;
        this.Ean = ean;
        this.Quantity = quantity;
        this.ImageUrl = imageUrl;
        this.Price = price;
        this.Description = description;
        this.WineType = wineType;
        this.Year = year;
        this.Volume = volume;
        this.AlcoholPercentage = alcoholPercentage;
        this.Country = country;
        this.Region = region;
        this.GrapeSort = grapeSort;
        this.Winery = winery;
        this.TastingNotes = tastingNotes;
    }

    /// <summary>
    /// Gets the list of suitable for enums
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<List<CustomEnum>> GetSuitableForAsCustomEnum(SharedContext context)
    {
        return await context.CustomEnums
            .Where(ce => context.ItemEnumRelations.Any(ier => ier.ItemId == this.Id && ier.CustomEnumId == ce.Id))
            .ToListAsync();
    }

    /// <summary>
    /// Gets the list of suitable for enums as a list of ints
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<List<int>> GetSuitableForAsIntList(SharedContext context)
    {
        return await context.CustomEnums
            .Where(ce => context.ItemEnumRelations.Any(ier => ier.ItemId == this.Id && ier.CustomEnumId == ce.Id))
            .Select(ce => ce.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Sets the list of suitable for enums
    /// </summary>
    /// <param name="context"></param>
    /// <param name="suitableForEnumIds"></param>
    public async Task SetSuitableFor(SharedContext context, List<int>? suitableForEnumIds)
    {
        await this.ClearSuitableFor(context);

        this.SuitableFor = suitableForEnumIds?.Select(id => new ItemEnumRelation()
        {
            ItemId = this.Id,
            Item = this,
            CustomEnumId = id,
            CustomEnum = context.CustomEnums.FirstOrDefault(e => e.Id == id)
        }).ToList();
    }

    /// <summary>
    /// Clears the list of suitable for enums and removes them from the database
    /// </summary>
    /// <param name="context">The context</param>
    public async Task ClearSuitableFor(SharedContext context)
    {
        var itemEnumRelations = await context.ItemEnumRelations.Where(ier => ier.ItemId == this.Id).ToListAsync();
        context.ItemEnumRelations.RemoveRange(itemEnumRelations);
        await context.SaveChangesAsync();
    }
}
