namespace Adm.Company.Application.ViewModel.Jwt;

public static class ConfiguracaoJwt
{
    public static string Key { get; private set; } = string.Empty;
    public static string Issue { get; private set; } = string.Empty;
    public static string Audience { get; private set; } = string.Empty;
    public static int Expiration { get; private set; }

    public static void Configure(string key, string issue, string audience, int expiration)
    {
        Key = key;
        Issue = issue;
        Audience = audience;
        Expiration = expiration;
    }
}
