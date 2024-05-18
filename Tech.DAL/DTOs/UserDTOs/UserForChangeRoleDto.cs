using System.Text.Json.Serialization;
using Tech.Domain.Enums.Users;

namespace Tech.DAL.DTOs.UserDTOs;

public class UserForChangeRoleDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("role")]
    public Roles Role { get; set; }
}
