using API.DataTransferObjects;
using API.Enums;
using API.Models.Items;

namespace API.Models.Authentication;

public class Admin : Employee
{
    public Admin(string firstName, string lastName, string phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        Role = Role.Admin;
    }

    public Admin(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Phone = user.Phone;
        Email = user.Email;
        Password = user.Password;
        Token = user.Token;
        Salt = user.Salt;
        Role = Role.Admin;
    }

    protected void ChangeAdmin(string firstName, string lastName, string phone, string email, string password, int? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        Role = Role.Admin;
    }

    public Wine CreateWine(ItemDto itemDto, List<CustomEnum> suitableFor)
    {
        Wine wine = new Wine(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price, itemDto.ItemDescription, itemDto.WineType, itemDto.Year, itemDto.Volume, itemDto.AlcoholPercentage, itemDto.Country, itemDto.Region, itemDto.GrapeSort, itemDto.Winery, itemDto.TastingNotes, suitableFor);
        return wine;
    }

    public Liquor CreateLiquor(ItemDto itemDto)
    {
        Liquor liquor = new Liquor(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price, itemDto.ItemDescription);
        return liquor;
    }

    public DefaultItem CreateDefaultItem(ItemDto itemDto)
    {
        DefaultItem defaultItem = new DefaultItem(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price, itemDto.ItemDescription);
        return defaultItem;
    }

}
