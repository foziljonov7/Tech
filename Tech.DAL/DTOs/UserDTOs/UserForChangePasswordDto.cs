using System.Text.Json.Serialization;

namespace Tech.DAL.DTOs.UserDTOs;

public class UserForChangePasswordDto
{
    [JsonPropertyName("old_password")]
    public string OldPassword { get; set; }
    [JsonPropertyName("new_password")]
    public string NewPassword { get; set; }
    [JsonPropertyName("confirm_password")]
    public string ConfirmPassword { get; set; }
}
