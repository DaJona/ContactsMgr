using System;
using System.IO;

namespace Utilities
{
    public class SitePaths
    {
        public static Func<string, string> ServerMapPath;

        public static string contactsPics(int memberId)
        {
            string basePath = "~/img/contactsPics/";
            string shaMemberDirectory = Encoding.sha1(memberId.ToString());
            string fullPath = basePath + shaMemberDirectory;

            if (!Directory.Exists(ServerMapPath(fullPath)))
            {
                Directory.CreateDirectory(ServerMapPath(fullPath));
            }

            return fullPath + "/";
        }
    }
}
