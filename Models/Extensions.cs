using OverlayImageForWindows.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static void SetVideoThumb(this System.Windows.Controls.Image self, string thumbName)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(thumbName));
            self.Source = bitmap;
        }

        public static void SetImage(this System.Windows.Controls.Image self, string imageName)
        {
            if (imageName.Contains("Main.jpg")) return;
            try
            {
                BitmapImage bitmap = new BitmapImage(new Uri(FileSystem.ImagePath + imageName));
                self.Source = bitmap;
            }
            catch (Exception ex)
            {
                new Log("При получении изображения получена ошибка - " + ex.Message);
            }
        }

        public static string GetNextName(this string name, string directoryPath)
        {
            var id = 0;
            while (File.Exists(directoryPath + id+ name))
            {
                id++;
            }
            return id + name;
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

        public static List<OverlayVideo> ToVideos(this FileInfo[] fileinfo)
        {
            var result = new List<OverlayVideo>();
            foreach (var item in fileinfo)
            {
                var vid = new OverlayVideo(item.FullName);
                vid.CreateThumbNail();
                result.Add(vid);
            }
            return result;
        }

        public static string GetImageType(this string fileName, string directoryPath)
        {
            var info = new DirectoryInfo(directoryPath).GetFiles();
            var img = info.FirstOrDefault(x => x.Name.Contains(fileName));
            if (img == default) return string.Empty;
            return img.FullName;
        }
    }
}
