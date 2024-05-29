using System.Text.Json.Serialization;

namespace Tech.DAL.DTOs.ExportDTOs;

public class FileResultDto
{
    [JsonPropertyName("contents")]
    public byte[] Contents { get; set; }
    [JsonPropertyName("content_type")]
    public string ContentType { get; set; }
    [JsonPropertyName("filename")]
    public string FileName { get; set; }
}
