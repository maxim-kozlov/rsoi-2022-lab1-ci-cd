namespace Persons.Gateway.Database;

public class RawPerson
{
    public string Name { get; set; } = null!;

    public string? Address { get; set; }
    
    public string? Work { get; set; }
    
    public int? Age { get; set; }
}