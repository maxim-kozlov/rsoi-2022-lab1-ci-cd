using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persons.Gateway.Database;
using Persons.Gateway.Domain;
using Persons.Gateway.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace Persons.Gateway.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v1/persons")]
public class PersonsController : ControllerBase
{
    private readonly ILogger<PersonsController> _logger;
    private readonly IPersonsRepository _personsRepository;
    private readonly IMapper _mapper;
    
    public PersonsController(ILogger<PersonsController> logger, IMapper mapper, IPersonsRepository personsRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _personsRepository = personsRepository;
    }

    /// <summary>
    /// Получить информацию о человеке
    /// </summary>
    /// <param name="personId">Идентификатор человека </param>
    /// <returns></returns>
    [HttpGet("{personId:int}")]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(PersonDto), description: "Пользователь найден.")]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Пользователь не найден.")]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ошибка на стороне сервера.")]
    public async Task<IActionResult> Get(int personId)
    {
        try
        {
            var person = await _personsRepository.GetPersonByIdAsync(personId);
            if (person == null)
                return NotFound(personId);
            return Ok(_mapper.Map<Person, PersonDto>(person));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error!");
            throw;
        }
    }
    
    /// <summary>
    /// Получить информацию по всем людям
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(IEnumerable<PersonDto>), description: "Пользователи.")]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ошибка на стороне сервера.")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var persons = await _personsRepository.GetPersonsAsync();
            return Ok(persons.Select(person => _mapper.Map<Person, PersonDto>(person)));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error!");
            throw;
        }
    }
    
    /// <summary>
    /// Создать новую запись о человеке
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [SwaggerResponse(statusCode: StatusCodes.Status201Created, type: typeof(PersonDto), description: "Созданный человек.")]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, description: "Невалидные данные.")]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ошибка на стороне сервера.")]
    public async Task<IActionResult> Create([FromBody] RawPerson rawPerson)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var person = await _personsRepository.CreatePersonAsync(rawPerson);
            return Created($"api/v1/persons/{person.Id}", _mapper.Map<Person, PersonDto>(person));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error!");
            throw;
        }
    }

    /// <summary>
    /// Удалить информацию о человеке
    /// </summary>
    /// <param name="personId">Идентификатор человека </param>
    /// <param name="rawPerson">Новая информация о человеке </param>
    /// <returns></returns>
    [HttpPatch("{personId:int}")]
    [SwaggerResponse(statusCode: StatusCodes.Status202Accepted, type: typeof(PersonDto), description: "Пользователь обновлён.")]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Пользователь не найден.")]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ошибка на стороне сервера.")]
    public async Task<IActionResult> Update(int personId, [FromBody] RawPerson rawPerson)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var person = await _personsRepository.UpdatePersonAsync(personId, rawPerson);
            if (person == null)
                return NotFound(personId);
            return Ok(_mapper.Map<Person, PersonDto>(person));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error!");
            throw;
        }
    }
    
    /// <summary>
    /// Удалить информацию о человеке
    /// </summary>
    /// <param name="personId">Идентификатор человека </param>
    /// <returns></returns>
    [HttpDelete("{personId:int}")]
    [SwaggerResponse(statusCode: StatusCodes.Status204NoContent, description: "Пользователь удалён.")]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, description: "Пользователь не найден.")]
    [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, description: "Ошибка на стороне сервера.")]
    public async Task<IActionResult> Remove(int personId)
    {
        try
        {
            var person = await _personsRepository.RemovePersonAsync(personId);
            if (person == null)
                return NotFound(personId);
            return NoContent(); // Accepted(_mapper.Map<Person, PersonDto>(person));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error!");
            throw;
        }
    }
}