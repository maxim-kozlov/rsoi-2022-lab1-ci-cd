using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persons.Gateway.Domain;

namespace Persons.Gateway.Database;

/// <inheritdoc />
public class PersonsRepository : IPersonsRepository
{
    private readonly PersonsContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PersonsRepository> _logger;

    public PersonsRepository(PersonsContext context, IMapper mapper, ILogger<PersonsRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetPersonsAsync()
    {
        try
        {
            var entities = await _context.Persons
                .AsNoTracking()
                .ToListAsync();

            return entities.Select(x => _mapper.Map<PersonEntity, Person>(x));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error get all persons");
            throw;
        }
    }
    
    public async Task<Person?> GetPersonByIdAsync(int id)
    {
        try
        {
            var entity = await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<PersonEntity?, Person?>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error get person by id: {id}", id);
            throw;
        }
    }
    
    public async Task<Person> CreatePersonAsync(RawPerson rawPerson)
    {
        try
        {
            var entity = _mapper.Map<RawPerson, PersonEntity>(rawPerson);
            await _context.Persons.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<PersonEntity, Person>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error create person: {person}", rawPerson);
            throw;
        }
    }
    
    public async Task<Person?> PatchPersonAsync(int personId, PatchPerson patchPerson)
    {
        try
        {
            var entity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == personId);
            if (entity == null)
                return null;
            
            _mapper.Map(patchPerson, entity);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<PersonEntity, Person>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error patch person: {person}", patchPerson);
            throw;
        }
    }
    
    public async Task<Person?> RemovePersonAsync(int personId)
    {
        try
        {
            var entity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == personId);
            if (entity == null)
                return null;

            _context.Persons.Remove(entity);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<PersonEntity, Person>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error remove person by id: {personId}", personId);
            throw;
        }
    }
}