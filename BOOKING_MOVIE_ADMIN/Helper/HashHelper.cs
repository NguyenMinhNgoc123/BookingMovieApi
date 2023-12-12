using System;
using System.Security.Cryptography;
using System.Text;

namespace BOOKING_MOVIE_ADMIN.Helper
{
    public class HashHelper
    {
        public static string CreateSHA256(string data, string secretKey)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(data);
            Byte[] keyBytes = encoding.GetBytes(secretKey);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}