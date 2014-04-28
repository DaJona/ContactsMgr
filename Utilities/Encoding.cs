using System;
using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public class Encoding
    {
        public static string sha1(string feed1, string feed2 = "")
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(feed1 + feed2);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash).Replace("/", "a").Replace("\\", "b");
        }

        public static string sha512(string feed1, string feed2 = "")
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();            
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(feed1 + feed2);
            byte[] hash = sha512.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash).Replace("/", "a").Replace("\\", "b");
        }
    }
}
