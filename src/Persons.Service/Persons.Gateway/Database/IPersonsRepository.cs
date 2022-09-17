using System.Collections.Generic;
using System.Threading.Tasks;
using Persons.Gateway.Domain;

namespace Persons.Gateway.Database;

public interface IPersonsRepository
{
    /// <summary>
    /// Получить всех людях
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Person>> GetPersonsAsync();
    
    /// <summary>
    /// Получить человека по id
    /// </summary>
    /// <returns></returns>
    Task<Person?> GetPersonByIdAsync(int id);
    
    /// <summary>
    /// Добавить человека
    /// </summary>
    /// <param name="rawPerson"></param>
    /// <returns></returns>
    Task<Person> CreatePersonAsync(RawPerson rawPerson);
    
    /// <summary>
    /// Обновить человека
    /// </summary>
    /// <param name="personId"></param>
    /// <param name="rawPerson"></param>
    /// <returns></returns>
    Task<Person?> UpdatePersonAsync(int personId, RawPerson rawPerson);
    
    /// <summary>
    /// Удалить человека
    /// </summary>
    /// <param name="personId"></param>
    /// <returns></returns>
    Task<Person?> RemovePersonAsync(int personId);
}