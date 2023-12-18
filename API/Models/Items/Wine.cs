using API.DataTransferObjects;
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
    public CustomEnum WineTypeEnum { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public Wine()
    {

    }

    /// <summary>
    /// Constructor for creating a new wine
    /// </summary>
    /// <param name="itemDto"></param>
    /// <param name="context"></param>
    public Wine(ItemDto itemDto, SharedContext context)
    {
        this.Name = itemDto.Name;
        this.Ean = itemDto.Ean;
        this.Quantity = itemDto.Quantity;
        this.ReservedQuantity = itemDto.ReservedQuantity;
        this.ImageUrl = itemDto.ImageUrl;
        this.Price = itemDto.Price;
        this.Description = itemDto.Description;
        this.Year = itemDto.Year;
        this.Volume = itemDto.Volume;
        this.AlcoholPercentage = itemDto.AlcoholPercentage;
        this.Country = itemDto.Country ?? "";
        this.Region = itemDto.Region ?? "";
        this.GrapeSort = itemDto.GrapeSort ?? "";
        this.Winery = itemDto.Winery ?? "";
        this.TastingNotes = itemDto.TastingNotes ?? "";
        this.ItemType = ItemType.Wine;

        if (itemDto.WineTypeEnum == null)
        {
            throw new Exception("Wine type cannot be null");
        }

        var existingEnum = context.CustomEnums.Find(itemDto.WineTypeEnum.Id);

        this.WineTypeEnum = existingEnum?? throw new Exception("Enum does not exist");
    }

    /// <summary>
    /// Function for changing the properties of a wine
    /// </summary>
    /// <param name="itemDto"></param>
    public void ChangeWineProperties(ItemDto itemDto)
    {
        this.Name = itemDto.Name;
        this.Ean = itemDto.Ean;
        this.Quantity = itemDto.Quantity;
        this.ReservedQuantity = itemDto.ReservedQuantity;
        this.ImageUrl = itemDto.ImageUrl;
        this.Price = itemDto.Price;
        this.Description = itemDto.Description;
        this.Year = itemDto.Year;
        this.Volume = itemDto.Volume;
        this.AlcoholPercentage = itemDto.AlcoholPercentage;
        this.Country = itemDto.Country ?? this.Country;
        this.Region = itemDto.Region ?? this.Region;
        this.GrapeSort = itemDto.GrapeSort ?? this.GrapeSort;
        this.Winery = itemDto.Winery ?? this.Winery;
        this.TastingNotes = itemDto.TastingNotes ?? this.TastingNotes;
        this.ReservedQuantity = itemDto.ReservedQuantity;
        this.WineTypeEnum = itemDto.WineTypeEnum ?? this.WineTypeEnum;
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
