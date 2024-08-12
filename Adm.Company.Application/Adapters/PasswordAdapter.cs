using static BCrypt.Net.BCrypt;

namespace Adm.Company.Application.Adapters;

public class PasswordAdapter
{
    public static bool VerifyPassword(string confirmPassword, string password)
    {
        return Verify(confirmPassword, password);
    }

    public static string GenerateHash(string password)
    {
        return HashPassword(password, 10);
    }
}
