using System.Text.Json.Serialization;

namespace Tech.DAL.DTOs.RegistryDTOs;

public class GetRegistryDto
{
    [JsonPropertyName("debit")]
    public double Debit { get; set; }
    [JsonPropertyName("credit")]
    public double Credit { get; set; }
}
