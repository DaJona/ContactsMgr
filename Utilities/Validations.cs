using System;
using System.Text.RegularExpressions;

namespace Utilities
{
    public class Validations
    {
        public static bool isValidContactPic(string fileExtension, int fileSizeInBytes)
        {
            string[] admitedExtensions = { ".jpg", ".bmp", ".png" };

            if (fileSizeInBytes <= 2097152) // 2097152 = 2Mb
            {
                return Array.Exists(admitedExtensions, s => s.Equals(fileExtension));
            }
            else
            {
                return false;
            }
        }

        #region Mail format

        public static bool isValidEmailFormat(string email)
        {
            // Only one @
            MatchCollection exAt = Regex.Matches(email, ("@"), RegexOptions.IgnoreCase);
            if (exAt.Count != 1)
                return false;

            // At least one dot
            if (!email.Contains("."))
                return false;

            // No spaces
            if (email.Contains(" "))
                return false;

            // No invalid characters
            string[] invalidCharacters = { "ñ", "Ñ", "á", "é", "í", "ó", "ú", "Á", "É", "Í", "Ó", "Ú" };
            foreach (string invalidChar in invalidCharacters)
            {
                if (email.Contains(invalidChar))
                {
                    return false;
                }
            }

            // Valid regex format
            string emailPattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            if (!Regex.IsMatch(email, emailPattern))
                return false;

            return true;
        }

        #endregion
    }
}
