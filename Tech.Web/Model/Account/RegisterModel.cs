using System.Text.Json.Serialization;

namespace Tech.Web.Model.Account;

public class RegisterModel
{
    [JsonPropertyName("firstname")]
    public string Firstname { get; set; } = string.Empty;
    [JsonPropertyName("lastname")]
    public string Lastname { get; set; } = string.Empty;
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
