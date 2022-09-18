using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Persons.Gateway.Dto;

public class RawPersonDto
{
    [Required, JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("address")]
    public string? Address { get; set; }
    
    [JsonPropertyName("work")]
    public string? Work { get; set; }
    
    [JsonPropertyName("age")]
    public int? Age { get; set; }
}