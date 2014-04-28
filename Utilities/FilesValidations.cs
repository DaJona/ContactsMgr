
using System;
namespace Utilities
{
    public class FilesValidations
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
    }
}
