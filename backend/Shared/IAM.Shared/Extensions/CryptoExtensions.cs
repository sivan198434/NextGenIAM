using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace IAM.Shared.Extensions;

public static class CryptoExtensions
{
    public static string CalculateHash(this object obj, string previousHash = "")
    {
        var json = JsonSerializer.Serialize(obj);
        var input = previousHash + json;
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }
}
