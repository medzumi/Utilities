using System.IO;

namespace medzumi.Utilities
{
    public static class DirectoryExtensions
    {
        public static void CreateFullDirectoryIfNeed(string path)
        {
            if (!Directory.Exists(path))
            {
                if (Directory.GetParent(path) is { } directoryInfo)
                {
                    if (!directoryInfo.Exists)
                    {
                        CreateFullDirectoryIfNeed(directoryInfo.FullName);
                    }
                }
                Directory.CreateDirectory(path);
            }
        }
    }
}