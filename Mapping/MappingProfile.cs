using API.DataTransferObjects;
using API.Models.Authentication;
using API.Models.Items;
using API.Models.Orders;
using API.Models.Suppliers;
using AutoMapper;

namespace API.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserStandardDto>().ReverseMap();
        CreateMap<ItemDto, Item>().ReverseMap();
        CreateMap<ItemDto, Liquor>().ReverseMap();
        CreateMap<ItemDto, DefaultItem>().ReverseMap();
        CreateMap<ItemDto, Wine>().ReverseMap();
        CreateMap<SupplierDto, Supplier>().ReverseMap();
        CreateMap<PurchaseOrderDto, PurchaseOrder>().ReverseMap();
        CreateMap<OrderLine, OrderLineDto>()
            .ForMember(dest => dest.ItemDto, opt => opt.MapFrom(src => src.Item))
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item!.Name))
            .ReverseMap();
    }
}
