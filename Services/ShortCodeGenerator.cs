using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Api.Services
{
    public static class ShortCodeGenerator
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string Generate(int length = 6)
        {
            var bytes = RandomNumberGenerator.GetBytes(length);
            var sb = new StringBuilder(length);
            foreach (var b in bytes)
            {
                sb.Append(Alphabet[b % Alphabet.Length]);
            }
            return sb.ToString();
        }
    }
}
