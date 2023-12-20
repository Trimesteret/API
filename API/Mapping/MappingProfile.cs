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
        CreateMap<PurchaseOrder, PurchaseOrderDto>().ReverseMap();
        CreateMap<InboundOrderDto, InboundOrder>();
        CreateMap<InboundOrder, InboundOrderDto>()
            .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));
        CreateMap<OrderLineDto, OrderLine>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.ItemDto))
            .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.ItemDto!.Price))
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.ItemDto!.Name));
        CreateMap<OrderLine, OrderLineDto>()
            .ForMember(dest => dest.ItemDto, opt => opt.MapFrom(src => src.Item));
    }
}
