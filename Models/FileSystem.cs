using Newtonsoft.Json;
using OverlayImageForWindows.Models.Data;
using OverlayImageForWindows.Models.Data.TelegramData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace OverlayImageForWindows.Models
{
    internal static class FileSystem
    {
        internal static readonly string MainPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OverlayImages\\",
            CFG = MainPath + "\\data.cfg",
            ImagePath = MainPath + "Images\\",
            LogPath = MainPath + "Logs.txt",
            DataFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ImagingConfig.json",
            VideoPath = MainPath + "Videos\\",
            ThumnailPath = VideoPath + "Thumbnails\\",
            UsersPath = MainPath + "users.json";

        public static Config config;
        public static UserInfo info;
        public static List<TelegramUser> users = new List<TelegramUser>();
        public static void Init(MainWindow w)
        {
            Directory.CreateDirectory(MainPath);
            Directory.CreateDirectory(ImagePath);
            Directory.CreateDirectory(VideoPath);
            Directory.CreateDirectory(ThumnailPath);
            if (!File.Exists(CFG))
            {
                try
                {
                    config = new Config()
                    {
                        ImagePath = "Main.jpg",
                        ScreenSize = new System.Numerics.Vector2(1920, 1080)
                    };
                    File.WriteAllText(CFG, JsonConvert.SerializeObject(config));
                }
                catch
                {
                    MessageBox.Show($"Пожалуйста проверьте файл '{CFG}' на целостность введенных данных");
                    throw new Exception();
                }

            }
            if (File.Exists(DataFile))
            {
                try
                {
                    info = JsonConvert.DeserializeObject<UserInfo>(File.ReadAllText(DataFile));
                    TG.Bot.Init();
                }
                catch
                {
                    MessageBox.Show($"Пожалуйста проверьте файл '{DataFile}' на целостность введенных данных");
                    throw new Exception();
                }
            }
            if (!File.Exists(UsersPath))
            {
                File.Create(UsersPath);
            }
            else
            {
                new Log("File doesnd exists");
                users = JsonConvert.DeserializeObject<List<TelegramUser>>(File.ReadAllText(UsersPath));
                if (users == null) users = new List<TelegramUser>();
            }
            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(CFG));
        }

        public static void SaveImage(string path)
        {
            File.WriteAllBytes(ImagePath + path.GetFileName2(),File.ReadAllBytes(path));
        }

        public static void SaveImage(string name, byte[] data)
        {
            File.WriteAllBytes(ImagePath + name, data);
        }

        public static string SaveVideo(string path)
        {
            File.WriteAllBytes(VideoPath + path.GetFileName2(), File.ReadAllBytes(path));
            return VideoPath + path.GetFileName2();
        }

        public static void Save(MainWindow w)
        {
            config.ScreenSize = new System.Numerics.Vector2((float)w.Width, (float)w.Height);
            config.IsVideo = w.MainImage.Visibility == Visibility.Visible ? false : true;
            if (!config.IsVideo) config.ImagePath = w.MainImage.Source.ToString().GetFileName();
            else config.ImagePath = w.MainVideo.Source.ToString().GetFileName();
            File.WriteAllText(CFG, JsonConvert.SerializeObject(config));
        }

        public static List<FileInfo> GetImages()
        {
            var result = new List<FileInfo>();
            var sb = new StringBuilder();
            foreach(var item in new DirectoryInfo(ImagePath).GetFiles())
            {
                if (item.FullName.IsImage())
                {
                    sb.Append(item.Name + "\n");
                    result.Add(item);
                }
            }
            return result.OrderBy(x=>x.Name.Length).ToList();
        }

        public static OverlayVideo CreateVideo(string videoPath)
        {
            var video = new OverlayVideo(videoPath);
            video.CreateThumbNail();
            return video;
        }

        public static void CreateUser(long id)
        {
            users.Add(new TelegramUser() { TelegramId = id });
            File.WriteAllText(UsersPath, JsonConvert.SerializeObject(users));
        }

        public static void SaveUsers() =>
             File.WriteAllText(UsersPath, JsonConvert.SerializeObject(users));

        private static bool IsImage(this string filePath)
        {
            try
            {
                using (var image = Image.FromFile(filePath))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
