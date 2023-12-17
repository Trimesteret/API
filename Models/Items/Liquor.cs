using API.DataTransferObjects;
using API.Enums;

namespace API.Models.Items;

public class Liquor: Item
{
    public int? Year { get; protected set; }
    public double? Volume { get; protected set; }
    public double? AlcoholPercentage { get; protected set; }
    public CustomEnum LiquorTypeEnum { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public Liquor()
    {

    }


    /// <summary>
    /// Constructor for creating a new liquor
    /// </summary>
    /// <param name="itemDto"></param>
    /// <param name="context"></param>
    /// <exception cref="Exception"></exception>
    public Liquor(ItemDto itemDto, SharedContext context)
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
        this.ItemType = ItemType.Liquor;

        if (itemDto.LiquorTypeEnum == null)
        {
            throw new Exception("Liquor type cannot be null");
        }

        var existingEnum = context.CustomEnums.Find(itemDto.LiquorTypeEnum.Id);

        this.LiquorTypeEnum = existingEnum ?? throw new Exception("Enum does not exist");
    }

    public void ChangeLiquorProperties(ItemDto itemDto)
    {
        this.Name = itemDto.Name;
        this.Ean = itemDto.Ean;
        this.Quantity = itemDto.Quantity;
        this.Year = itemDto.Year;
        this.AlcoholPercentage = itemDto.AlcoholPercentage;
        this.Volume = itemDto.Volume;
        this.ImageUrl = itemDto.ImageUrl;
        this.Price = itemDto.Price;
        this.Description = itemDto.Description;
        this.LiquorTypeEnum = itemDto.LiquorTypeEnum ?? this.LiquorTypeEnum;
    }
}
