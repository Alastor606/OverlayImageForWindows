using OverlayImageForWindows.Models.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace OverlayImageForWindows.Models
{
    internal static class Extensions
    {
        public static string GetFileName(this string path)
        {
            return path.Split('/').Last();
        }

        public static string GetFileName2(this string path)
        {
            return path.Split('\\').Last();
        }

        public static void SetImage(this System.Windows.Controls.Image self, string imageName)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(FileSystem.ImagePath + imageName));
            self.Source = bitmap;
        }

        public static string GetNextName(this string name)
        {
            var newName = new StringBuilder();
            var id = 0;
            newName.Append(name);
            while (File.Exists(FileSystem.ImagePath +id+ newName))
            {
                id++;
            }
            return id + newName.ToString();
        }

        public static List<ImageCast> ToImageCast(this FileInfo[] fileinfo)
        {
            var result = new List<ImageCast>();
            for (int i = 0; i < fileinfo.Length; i+= 2)
            {
                if (i + 1 <= fileinfo.Length) result.Add(new ImageCast(fileinfo[i].FullName, fileinfo[i + 1].FullName));
                else result.Add(new ImageCast(fileinfo[i].FullName, "empty"));
            }
            return result;
        }
    }
}
