using API.Enums;

namespace API.Models.Items;

public abstract class Item
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public string Ean { get; protected set; }
    public int Quantity { get; protected set; }
    public int ReservedQuantity { get; protected set; }
    public double Price { get; protected set; }
    public string? ImageUrl { get; protected set; }
    public string Description { get; protected set; }
    public float Mass { get; protected set; }
    public ItemType ItemType { get; protected set; }


    /// <summary>
    /// Reserving a quantity of an item
    /// </summary>
    /// <param name="quantityToReserve"></param>
    public void ReserveQuantity(int quantityToReserve)
    {
        if (this.ReservedQuantity + quantityToReserve > Quantity)
        {
            throw new Exception("Not enough items in stock to reserve that amount");
        }

        this.ReservedQuantity += quantityToReserve;
    }
}


