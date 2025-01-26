using OverlayImageForWindows.Models;
using OverlayImageForWindows.Models.Data;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OverlayImageForWindows.Views
{
    public partial class GetMediaWindow : Window
    {
        public Action<string, bool> OnMediaPicked;
        private bool isVideoLoaded = false;

        public GetMediaWindow()
        {
            InitializeComponent();
            LoadImages();
        }

        private async void LoadImages()
        {
            ImageSW.Visibility = Visibility.Visible;
            VideoSW.Visibility = Visibility.Hidden;
            if (ImageGrid.Children.Count > 0) return;

            foreach (var image in FileSystem.GetImages())
            {
                Image img = null;
                try
                {
                    img = new Image()
                    {
                        Width = 100,
                        Height = 150,
                        Name = "A_" + image.Name.Split('.')[0],
                        Margin = new Thickness(10, 0, 0, 0)
                    };
                    img.SetImage(image.Name);
                }
                catch (Exception ex)
                {
                    new Log(ex.Message);
                    var name = "TgFileName.png".GetNextName(FileSystem.ImagePath);
                    File.Copy(image.FullName, FileSystem.ImagePath + name);
                    File.Delete(image.FullName);
                    img = new Image()
                    {
                        Width = 100,
                        Height = 150,
                        Name = "A_" + name.Split('.')[0],
                        Margin = new Thickness(10, 0, 0, 0)
                    };
                    img.SetImage(name);
                }
                img.MouseLeftButtonDown += delegate
                {
                    OnMediaPicked?.Invoke(img.Name, false);
                    this.Close();
                };
                ImageGrid.Children.Add(img);
                await Task.Delay(90);
            }
        }

        private void LoadVideos()
        {
            VideoSW.Visibility = Visibility.Visible;
            ImageSW.Visibility = Visibility.Collapsed;
            if (VideoGrid.Children.Count > 0)return;

            try
            {
                foreach (var item in new DirectoryInfo(FileSystem.VideoPath).GetFiles())
                {
                    Image img = null;
                    string fullName = string.Empty;
                    try
                    {
                        img = new Image()
                        {
                            Width = 100,
                            Height = 150,
                            Name = "A_" + item.Name.Split('.')[0],
                            Margin = new Thickness(10, 0, 0, 0)
                        };
                        fullName = item.FullName;
                    }
                    catch (Exception ex)
                    {
                        new Log("При попытке задать имя видео произошла ошибка - " + ex.Message);

                        var name = "TgFileName.mp4".GetNextName(FileSystem.VideoPath);

                        File.Copy(item.FullName, FileSystem.VideoPath + name);
                        File.Delete(item.FullName);
                        File.Delete(item.FullName + "-thumbnail.png");
                        img = new Image()
                        {
                            Width = 100,
                            Height = 150,
                            Name = "A_" + name.Split('.')[0],
                            Margin = new Thickness(10, 0, 0, 0)
                        };
                        fullName = FileSystem.VideoPath + name;
                    }

                    var video = FileSystem.CreateVideo(fullName);
                    img.SetVideoThumb(video.ThumNailPath);
                    img.MouseLeftButtonDown += delegate
                    {
                        OnMediaPicked?.Invoke(img.Name, true);
                        this.Close();
                    };
                    VideoGrid.Children.Add(img);
                }
            }
            catch (Exception ex)
            {
                new Log(ex.Message);
            }
            
        }

        private void Images_Click(object sender, RoutedEventArgs e)
        {
            if (!isVideoLoaded) return;
            isVideoLoaded = false;
            Images.IsEnabled = false;
            Videos.IsEnabled = true;
            LoadImages();
        }

        private void Videos_Click(object sender, RoutedEventArgs e)
        {
            if (isVideoLoaded) return;
            isVideoLoaded = true;
            Videos.IsEnabled = false;
            Images.IsEnabled = true;
            LoadVideos();
        }
    }
}
