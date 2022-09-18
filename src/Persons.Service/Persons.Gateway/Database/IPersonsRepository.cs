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
    /// <param name="patchPerson">Модель обновления записи о человеке </param>
    /// <returns></returns>
    Task<Person?> PatchPersonAsync(int personId, PatchPerson patchPerson);
    
    /// <summary>
    /// Удалить человека
    /// </summary>
    /// <param name="personId"></param>
    /// <returns></returns>
    Task<Person?> RemovePersonAsync(int personId);
}