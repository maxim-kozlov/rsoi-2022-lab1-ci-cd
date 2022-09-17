using AutoMapper;
using Persons.Gateway.Mapper;

namespace Persons.Tests;

public static class MapperFixture
{
    public static IMapper Mapper { get; }

    static MapperFixture()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new PersonProfile());
        });
        Mapper = mappingConfig.CreateMapper();
    }
}