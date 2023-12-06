using API.Models.Items;

namespace API.Enums;

public class ItemEnumRelation
{
    public int ItemEnumRelationId { get; set; }

    public int ItemId { get; set; }
    public Item Item { get; set; }

    public int CustomEnumId { get; set; }
    public CustomEnum? CustomEnum { get; set; }

    /**
     * Empty constructor for EF Core
     */
    public ItemEnumRelation()
    {

    }

    public ItemEnumRelation(Item item, int cEnumId)
    {
        this.Item = item;
        this.ItemId = item.Id;
        this.CustomEnumId = cEnumId;
    }
}
