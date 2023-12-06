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

    public Wine CreateWine(ItemDto itemDto)
    {
        Wine wine = new Wine(itemDto.Name, itemDto.Ean, itemDto.Quantity, itemDto.Price, itemDto.Description, itemDto.WineType, itemDto.Year, itemDto.Volume, itemDto.AlcoholPercentage, itemDto.Country, itemDto.Region, itemDto.GrapeSort, itemDto.Winery, itemDto.TastingNotes);
        return wine;
    }

    public void SetSuitableForOnWine(Wine wine, List<int> suitableForEnumIds)
    {
        List<ItemEnumRelation> itemEnumRelations = new List<ItemEnumRelation>();

        foreach (var cEnum in suitableForEnumIds)
        {
            itemEnumRelations.Add(new ItemEnumRelation(wine, cEnum));
        }

        wine.SetSuitableFor(itemEnumRelations);
    }

    public Liquor CreateLiquor(ItemDto itemDto)
    {
        Liquor liquor = new Liquor(itemDto.Name, itemDto.Ean, itemDto.Quantity, itemDto.Price, itemDto.Description);
        return liquor;
    }

    public DefaultItem CreateDefaultItem(ItemDto itemDto)
    {
        DefaultItem defaultItem = new DefaultItem(itemDto.Name, itemDto.Ean, itemDto.Quantity, itemDto.Price, itemDto.Description);
        return defaultItem;
    }

}
