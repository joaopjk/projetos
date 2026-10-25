using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services;

public class PasswordEncripter
{
    public string Encript(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        return string.Join("", bytes.Select(x => x.ToString("X2")));
    }
}