using System.Text;
using API.DataTransferObjects;
using API.Enums;
using API.Mapping;
using API.Models;
using API.Models.Items;
using API.Models.Suppliers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Tests;

public class SharedTesting
{
    public static IMapper GetMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        return configuration.CreateMapper();
    }

    public static SharedContext GetContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<SharedContext>().UseInMemoryDatabase(databaseName: "InMemoryDatabase").Options;

        return new SharedContext(dbContextOptions);
    }

    static int GetRandomInt()
    {
        var random = new Random();
        return random.Next(100, 1000000);
    }

    static string GetRandomString(int minLength = 8, int maxLength = 28)
    {
        var random = new Random();

        random.Next(minLength, maxLength);

        var stringRandom = new StringBuilder();
        for (int i = 0; i < 16; i++)
        {
            stringRandom.Append((char)random.Next(33, 126));
        }

        return stringRandom.ToString();
    }

    static T GetRandomEnum<T>()
    {
        var enumValues = Enum.GetValues(typeof(T));
        var random = new Random();
        return (T)enumValues.GetValue(random.Next(enumValues.Length));
    }

    static async Task<CustomEnum> GetRandomCustomEnum(SharedContext context, EnumType enumType, bool createEnum = true)
    {
        var customEnum = new CustomEnum(enumType, GetRandomString(), GetRandomString());

        if(!createEnum)
        {
            return customEnum;
        }

        context.CustomEnums.Add(customEnum);
        await context.SaveChangesAsync();
        return customEnum;
    }

    static SupplierDto GetRandomSupplierDto(SharedContext context, IMapper mapper)
    {
        var supplier = new Supplier(GetRandomString());
        context.Suppliers.Add(supplier);

        return mapper.Map<SupplierDto>(supplier);
    }

    public async static Task<OrderLineDto> GetRandomOrderLineDto(SharedContext context, IMapper mapper)
    {
        var itemDto = await GetRandomItemDto(context, mapper);

        return new OrderLineDto
        {
            ItemDto = itemDto,
            ItemId = itemDto.Id,
            ItemName = itemDto.Name,
            ItemPrice = itemDto.Price,
            Quantity = GetRandomInt(),
            LinePrice = itemDto.Price * itemDto.Quantity
        };
    }

    public static async Task<ItemDto> GetRandomItemDto(SharedContext context, IMapper mapper, ItemType? itemType = null, bool createItem = true)
    {
        var suitableFor = new List<CustomEnum>
        {
            await GetRandomCustomEnum(context, EnumType.SuitableFor),
            await GetRandomCustomEnum(context, EnumType.SuitableFor),
            await GetRandomCustomEnum(context, EnumType.SuitableFor)
        };

        if (!itemType.HasValue)
        {
            itemType = GetRandomEnum<ItemType>();
        }

        if (itemType == null)
        {
            throw new Exception("itemType is null");
        }

        ItemType itemTypeValue = itemType.Value;


        var itemDto = new ItemDto
        {
            Ean = GetRandomInt().ToString(),
            Quantity = GetRandomInt(),
            Country = GetRandomString(),
            Description = GetRandomString(),
            Name = GetRandomString(),
            Price = GetRandomInt(),
            Region = GetRandomString(),
            Volume = GetRandomInt(),
            AlcoholPercentage = GetRandomInt(),
            GrapeSort = GetRandomString(),
            Winery = GetRandomString(),
            TastingNotes = GetRandomString(),
            SuitableForEnumIds = new List<int> { suitableFor[0].Id, suitableFor[1].Id, suitableFor[2].Id },
            ItemType = itemTypeValue,
            WineTypeEnum = await GetRandomCustomEnum(context, EnumType.WineType),
            LiquorTypeEnum = await GetRandomCustomEnum(context, EnumType.LiqourType)
        };

        await context.SaveChangesAsync();

        if (!createItem)
        {
            return itemDto;
        }

        Item item = null!;
        switch (itemDto.ItemType)
        {
            case ItemType.Liquor:
                item = new Liquor(itemDto, context);
                break;
            case ItemType.Wine:
                Wine wine = new Wine(itemDto, context);
                await wine.SetSuitableFor(context, itemDto.SuitableForEnumIds);
                item = wine;
                break;
            case ItemType.DefaultItem:
                item = new DefaultItem(itemDto);
                break;
        }

        context.Items.Add(item);
        await context.SaveChangesAsync();
        return mapper.Map<ItemDto>(item);
    }

    public static async Task<InboundOrderDto> GetRandomInboundOrderDto(SharedContext context, IMapper mapper, int minOrderLines = 1, int maxOrderLines = 8)
    {
        var orderLines = new List<OrderLineDto>();
        var random = new Random();

        for (int i = 0; i < random.Next(minOrderLines, maxOrderLines); i++)
        {
            orderLines.Add(await GetRandomOrderLineDto(context, mapper));
        }

        var supplierDto = GetRandomSupplierDto(context, mapper);
        await context.SaveChangesAsync();

        return new InboundOrderDto
        {
            DeliveryDate = DateTime.Now.AddDays(random.Next(1, 100)),
            OrderDate = DateTime.Now.AddDays(random.Next(1, 100)),
            OrderLines = orderLines,
            SupplierName = supplierDto.Name,
            Supplier = supplierDto,
            TotalPrice = orderLines.Sum(ol => ol.LinePrice)
        };
    }
}