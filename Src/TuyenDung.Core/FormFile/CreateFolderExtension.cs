using System;
using System.IO;

namespace TuyenDung.Core.FormFile
{
    public static class CreateFolderExtension
    {
        public static void CreateFolder(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Console.WriteLine(" Path already exists ! ");
                }
                else
                    Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}