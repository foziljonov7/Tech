using System.Text.Json.Serialization;
using Tech.Domain.Enums.Users;

namespace Tech.DAL.DTOs.UserDTOs;

public class UserForUpdateDto
{
	[JsonPropertyName("firstname")]
	public string Firstname { get; set; }
	[JsonPropertyName("lastname")]
	public string Lastname { get; set; }
	[JsonPropertyName("phone_number")]
	public string PhoneNumber { get; set; }
	[JsonPropertyName("password")]
	public string Password { get; set; }
	[JsonPropertyName("salt")]
    public string Salt { get; set; }
	[JsonPropertyName("role")]
    public Roles Role { get; set; }
}
