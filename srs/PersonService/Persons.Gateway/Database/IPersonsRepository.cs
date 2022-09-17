using System.Collections.Generic;
using System.Threading.Tasks;
using Persons.Gateway.Domain;

namespace Persons.Gateway.Database;

public interface IPersonsRepository
{
    Task<IEnumerable<Person>> GetPersons();
    Task<Person?> GetPersonById(int id);
    Task<Person> AddPerson(RawPerson rawPerson);
    Task<Person?> UpdatePerson(int personId, RawPerson rawPerson);
    Task<Person?> RemovePerson(int personId);
}