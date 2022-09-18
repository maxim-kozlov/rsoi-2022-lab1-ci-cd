namespace Persons.Gateway.Domain;

/// <summary>
/// Модель частичного обновления полей записи человека
/// </summary>
/// <remarks>null - поле игнорируется </remarks>
public class PatchPerson
{
    public string? Name { get; set; }

    public string? Address { get; set; }
    
    public string? Work { get; set; }
    
    public int? Age { get; set; }
}