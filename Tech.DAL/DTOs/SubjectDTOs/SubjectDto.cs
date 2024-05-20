using System.Text.Json.Serialization;

namespace Tech.DAL.DTOs.SubjectDTOs;

public class SubjectDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("category_id")]
    public long CategoryId { get; set; }
}
