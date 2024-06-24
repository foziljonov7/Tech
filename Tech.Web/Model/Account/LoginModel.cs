using System.Text.Json.Serialization;

namespace Tech.Web.Model.Account;

public class LoginModel
{
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
