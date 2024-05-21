using System.Text.Json.Serialization;

namespace Tech.DAL.DTOs.CategoryDTOs;

public class CategoryDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
