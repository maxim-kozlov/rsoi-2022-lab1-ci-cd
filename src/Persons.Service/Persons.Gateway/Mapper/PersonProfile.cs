using AutoMapper;
using Persons.Gateway.Database;
using Persons.Gateway.Domain;
using Persons.Gateway.Dto;

namespace Persons.Gateway.Mapper;

public class PersonProfile: Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<RawPersonDto, RawPerson>();

        CreateMap<Person, PersonEntity>();
        CreateMap<RawPerson, PersonEntity>();
        CreateMap<PersonEntity, Person>();
    }
}