using System.IO;

namespace WalloneInstaller.Services
{
    public class UriService
    {
        private static string path;

        public static void SetPath(string path)
        {
            UriService.path = path;
        }
        public static string GetPath()
        {
            return path;
        }
    }
}