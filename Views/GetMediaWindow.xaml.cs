using OverlayImageForWindows.Models;
using System;
using System.IO;
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

        private void LoadImages()
        {
            MainGrid.Children.Clear();
            foreach (var image in FileSystem.GetImages())
            {
                var img = new Image()
                {
                    Width = 100,
                    Height = 150,
                    Name = "A_" + image.Name.Split('.')[0],
                    Margin = new Thickness(10, 0, 0, 0)
                };
                img.SetImage(image.Name);
                img.MouseLeftButtonDown += delegate
                {
                    OnMediaPicked?.Invoke(img.Name, false);
                    this.Close();
                };
                MainGrid.Children.Add(img);
            }
        }

        private void LoadVideos()
        {
            MainGrid.Children.Clear();
            foreach(var item in new DirectoryInfo(FileSystem.VideoPath).GetFiles())
            {
                var video = FileSystem.CreateVideo(item.FullName);
                var img = new Image()
                {
                    Width = 100,
                    Height = 150,
                    Name = "A_" + video.FullName.GetFileName2().Split('.')[0],
                    Margin = new Thickness(10, 0, 0, 0)
                };
                img.SetVideoThumb(video.ThumNailPath);
                img.MouseLeftButtonDown += delegate
                {
                    OnMediaPicked?.Invoke(img.Name, true);
                    this.Close();
                };
                MainGrid.Children.Add(img);
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
