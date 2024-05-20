using System.Text.Json.Serialization;

namespace Tech.DAL.DTOs.SubjectDTOs;

public class SubjectForUpdateDto
{
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("category_id")]
	public long CategoryId { get; set; }
}
