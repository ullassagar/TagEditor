using System;
using System.IO;
using System.Linq;

namespace TagEditor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args == null || args.Count() == 0)
            {
                Console.WriteLine("Path not found.");
                Console.ReadLine();
                return;
            }

            var path = args[0];
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Path is empty.");
                Console.ReadLine();
                return;
            }

            if (!Directory.Exists(path))
            {
                Console.WriteLine("Path not exists: " + path);
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Started fixing album name(s) for: " + path);
            SetAlbumNames(path);
            Console.WriteLine("Completed successfully!!!");
            Console.ReadLine();
        }

        private static void SetAlbumNames(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                SetAlbumName(file);
            }

            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                SetAlbumNames(directory);
            }
        }

        private static void SetAlbumName(string file)
        {
            //try
            //{
            //    using (var tagFile = TagLib.File.Create(file))
            //    {
            //        tagFile.Tag.Clear();
            //        tagFile.Save();
            //    }
            //}
            //catch { }

            try
            {
                using (var tagFile = TagLib.File.Create(file))
                {
                    var info = new FileInfo(file);
                    tagFile.Tag.Album = info.Directory.Name;
                    //tagFile.Tag.Title = info.Name;
                    tagFile.Save();
                }
            }
            catch { }
        }
    }
}
