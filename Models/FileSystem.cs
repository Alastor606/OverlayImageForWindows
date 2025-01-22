using Newtonsoft.Json;
using OverlayImageForWindows.Models.Data;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace OverlayImageForWindows.Models
{
    internal static class FileSystem
    {
        internal static readonly string MainPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OverlayImages\\",
            CFG = MainPath + "\\data.cfg",
            ImagePath = MainPath + "Images\\",
            LogPath = MainPath + "Logs.txt",
            DataFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ImagingConfig.json";

        public static Config config;
        public static UserInfo info;
        public static void Init(MainWindow w)
        {
            Directory.CreateDirectory(MainPath);
            Directory.CreateDirectory(ImagePath);
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

        public static void Save(MainWindow w, string path)
        {
            config.ScreenSize = new System.Numerics.Vector2((float)w.Width, (float)w.Height);
            config.ImagePath = path;
            File.WriteAllText(CFG, JsonConvert.SerializeObject(config));
        }
    }
}
