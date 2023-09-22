using System.Security.Cryptography;
using System.Text;

namespace RealEstateAgency.Services.Extensions
{
    public static class StringExtensions
    {
        public static string GetSha256(this string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var inputHash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }
        public static string GetGenerateToken(this string token) 
        {
            int sizeToken = 6;
            while (sizeToken > 0)
            {
                token += (char)new Random().Next(0x0030, 0x007A); // Генерация от 0 до 9 и A-Z, a-z
                sizeToken--;
            }
            return token;
        }
    }
}
