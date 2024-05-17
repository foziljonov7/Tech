namespace Tech.Services.Helpers;

public class PasswordHelper
{
    public const string Key = "6c9e6964-7425-40de-944b-e07fc1f90ae3";
    public static (string Hash, string Salt) Hash(string password)
    {
        string salt = GenerateSalt();
        string hash = BCrypt.Net.BCrypt.HashPassword(salt + password + Key);
        return (hash: hash, salt: salt);
    }

    public static bool Verify(string password, string salt, string hash)
        => BCrypt.Net.BCrypt.Verify(salt + password + Key, hash);

	private static string GenerateSalt()
        => Guid.NewGuid().ToString();
}
