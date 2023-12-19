using API.Models.Items;

namespace API.Enums;

public class ItemEnumRelation
{
    public int ItemEnumRelationId { get; set; }

    public int ItemId { get; set; }
    public Item Item { get; set; }

    public int CustomEnumId { get; set; }
    public CustomEnum? CustomEnum { get; set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public ItemEnumRelation()
    {
        this.Item = null!;
    }

    public ItemEnumRelation(Item item, int cEnumId)
    {
        this.Item = item;
        this.ItemId = item.Id;
        this.CustomEnumId = cEnumId;
    }
}
