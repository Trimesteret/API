namespace API.Models.Orders;

public class Address
{
    public int Id { get; set; }
    public string AddressLine { get; set; }
    public string? Floor { get; set; }
    public string? Door { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    /// <summary>
    /// Parameterless constructor for EF Core
    /// </summary>
    public Address()
    {
        this.AddressLine = "";
        this.ZipCode = "";
        this.City = "";
        this.Country = "";
    }
}
