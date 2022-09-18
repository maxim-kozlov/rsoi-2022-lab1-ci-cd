using System.Text.Json.Serialization;

namespace Persons.Gateway.Dto;

public class PatchPersonDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }
    
    [JsonPropertyName("work")]
    public string? Work { get; set; }
    
    [JsonPropertyName("age")]
    public int? Age { get; set; }
}