using API.DataTransferObjects;
using API.Models.Items;
using API.Models.Suppliers;
using AutoMapper;

namespace API.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Authentication.User, DataTransferObjects.UserStandardDto>();
        CreateMap<DataTransferObjects.UserStandardDto, Models.Authentication.User>();
        CreateMap<ItemDto, Item>();
        CreateMap<Item, ItemDto>();
        CreateMap<ItemDto, Liquor>();
        CreateMap<Liquor, ItemDto>();
        CreateMap<ItemDto, DefaultItem>();
        CreateMap<DefaultItem, ItemDto>();
        CreateMap<ItemDto, Wine>();
        CreateMap<Wine, ItemDto>();
        CreateMap<SupplierDto, Supplier>();
        CreateMap<Supplier, SupplierDto>();
    }
}
