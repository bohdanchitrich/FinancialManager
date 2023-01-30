using System.Text.Json.Serialization;

namespace UI.Models.Shared;

public class UserModel
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

}