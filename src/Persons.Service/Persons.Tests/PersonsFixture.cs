using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Persons.Gateway.Database;

namespace Persons.Tests;

public class PersonsFixture : IAsyncDisposable
{
    public PersonsContext PersonsContext { get; }
    
    public IPersonsRepository PersonsRepository { get; }

    public PersonsFixture()
    {
        var options = new DbContextOptionsBuilder<PersonsContext>()
            .UseInMemoryDatabase("PersonsControllerTest")
            .Options;

        PersonsContext = new PersonsContext(options);
        
        PersonsContext.Database.EnsureDeleted();
        PersonsContext.Database.EnsureCreated();

        PersonsRepository = new PersonsRepository(PersonsContext, MapperFixture.Mapper, NullLogger<PersonsRepository>.Instance);
    }

    public ValueTask DisposeAsync()
    {
        return PersonsContext.DisposeAsync();
    }
}