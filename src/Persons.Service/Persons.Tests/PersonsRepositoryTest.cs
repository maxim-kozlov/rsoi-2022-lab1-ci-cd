using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Persons.Gateway.Database;
using Persons.Gateway.Domain;
using Xunit;
using Person = Persons.Gateway.Domain.Person;

namespace Persons.Tests;

public class PersonsRepositoryTest
{
    [Fact]
    public async Task CreateOkTest()
    {
        // arrange
        var fixture = new PersonsFixture();
        var faker = CreateFakerRawPerson();

        var rawPerson = faker.Generate();

        // act
        var result = await fixture.PersonsRepository.CreatePersonAsync(rawPerson);

        // assert
        var entity = await fixture.PersonsContext.Persons.FirstOrDefaultAsync(x => x.Id == result.Id);
        Assert.NotNull(entity);
        
        AssertEqual(rawPerson, result);
        AssertEqual(entity, result);
    }

    [Fact]
    public async Task GetAllOkTest()
    {
        // arrange
        int size = 5;
        
        var fixture = new PersonsFixture();
        var faker = CreateFakerPersonEntity();
        var entities = faker.Generate(size);
        await fixture.PersonsContext.Persons.AddRangeAsync(entities);
        await fixture.PersonsContext.SaveChangesAsync();
        
        // act
        var result = (await fixture.PersonsRepository.GetPersonsAsync()).ToList();
        
        // assert
        Assert.Equal(result.Count, size);
        for (int i = 0; i < size; i++)
            AssertEqual(entities[i], result[i]);
    }
    
    [Fact]
    public async Task GetByIdOkTest()
    {
        // arrange
        var fixture = new PersonsFixture();
        var faker = CreateFakerPersonEntity();
        var entities = faker.Generate(5);
        await fixture.PersonsContext.Persons.AddRangeAsync(entities);
        await fixture.PersonsContext.SaveChangesAsync();
        
        var person = entities[^1];
        
        // act
        var result = await fixture.PersonsRepository.GetPersonByIdAsync(person.Id);
        
        // assert
        Assert.NotNull(result);
        AssertEqual(person, result);
    }
    
    [Fact]
    public async Task GetByIdNotFoundTest()
    {
        // arrange
        var fixture = new PersonsFixture();
        
        // act
        var result = await fixture.PersonsRepository.GetPersonByIdAsync(-1);
        
        // assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task RemoveByIdOkTest()
    {
        // arrange
        var fixture = new PersonsFixture();
        var faker = CreateFakerPersonEntity();
        var entities = faker.Generate(2);
        await fixture.PersonsContext.Persons.AddRangeAsync(entities);
        await fixture.PersonsContext.SaveChangesAsync();
        
        var person = entities[^1];
        
        // act
        var result = await fixture.PersonsRepository.RemovePersonAsync(person.Id);
        
        // assert
        Assert.NotNull(result);
        Assert.Null(await fixture.PersonsContext.Persons.FirstOrDefaultAsync(x => x.Id == person.Id));
        AssertEqual(person, result);
    }
    
    [Fact]
    public async Task RemoveByIdNotFoundTest()
    {
        // arrange
        var fixture = new PersonsFixture();

        // act
        var result = await fixture.PersonsRepository.RemovePersonAsync(-1);
        
        // assert
        Assert.Null(result);
    }
    
    private static Faker<PersonEntity> CreateFakerPersonEntity()
    {
        return new Faker<PersonEntity>("ru")
            .RuleFor(x => x.Address, f => f.Address.StreetAddress().OrNull(f, 0.1f))
            .RuleFor(x => x.Age, f => f.Random.Number(0, 100).OrNull(f, 0.1f))
            .RuleFor(x => x.Name, f => f.Person.FullName)
            .RuleFor(x => x.Work, f => f.Person.Company.Name.OrNull(f, 0.1f))
            .RuleFor(x => x.Id, f => f.UniqueIndex + 1);
    }
    
    private static Faker<RawPerson> CreateFakerRawPerson()
    {
        return new Faker<RawPerson>("ru")
            .RuleFor(x => x.Address, f => f.Address.StreetAddress().OrNull(f, 0.1f))
            .RuleFor(x => x.Age, f => f.Random.Number(0, 100).OrNull(f, 0.1f))
            .RuleFor(x => x.Name, f => f.Person.FullName)
            .RuleFor(x => x.Work, f => f.Person.Company.Name.OrNull(f, 0.1f));
    }

    private static void AssertEqual(RawPerson expected, Person actual)
    {
        Assert.Equal(expected.Address, actual.Address);
        Assert.Equal(expected.Age, actual.Age);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Work, actual.Work);
    }
    
    private static void AssertEqual(PersonEntity expected, Person actual)
    {
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Address, actual.Address);
        Assert.Equal(expected.Age, actual.Age);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Work, actual.Work);
    }
}