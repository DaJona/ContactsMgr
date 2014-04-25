using System;
using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public class Encoding
    {
        public static string getHashedPassword(string memberUser, string memberPassword)
        {
            string feed = "7f9facc418f74439c5e9709832;0ab8a5:OCOdN5Wl,q8SLIQz8i|8agmu¬s13Q7ZXyno/";
            SHA512 sha512 = new SHA512CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(memberUser + memberPassword + feed);
            byte[] hash = sha512.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
    }
}
