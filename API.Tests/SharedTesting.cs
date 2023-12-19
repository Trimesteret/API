using API.Mapping;
using API.Models;
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
}