using OverlayImageForWindows.Models;
using OverlayImageForWindows.Models.Data;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

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

        private Image CreateImage(string name)
        {
            return new Image()
            {
                Width = 100,
                Height = 150,
                Name = "A_" + name,
                Margin = new Thickness(10, 0, 0, 0)
            };
        }

        private async void LoadImages()
        {
            ImageSW.Visibility = Visibility.Visible;
            VideoSW.Visibility = Visibility.Hidden;
            if (ImageGrid.Children.Count > 0) return;
            var images = FileSystem.GetImages();
            foreach (var image in images)
            {
                Image img = null;
                try
                {
                    img = CreateImage(image.Name.Split('.')[0]);
                    img.SetImage(image.Name);
                }
                catch (Exception ex)
                {
                    new Log(ex.Message);
                    var name = "TgFileName.png".GetNextName(FileSystem.ImagePath);
                    File.Copy(image.FullName, FileSystem.ImagePath + name);
                    File.Delete(image.FullName);
                    img = CreateImage(name.Split('.')[0]);
                    img.SetImage(name);
                }
                img.MouseLeftButtonDown += delegate
                {
                    OnMediaPicked?.Invoke(img.Name, false);
                    this.Close();
                };
                ImageGrid.Children.Add(img);
                await Task.Yield();
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
                    string name = item.Name.Split('.')[0];

                    if (!item.Name.Contains("TgFileName"))
                    {
                        fullName = RenameVideo(item.FullName);
                        name = fullName.Split('.')[0];
                        
                    }
                    else fullName = item.FullName;

                    img = CreateImage(name);
                    
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
                new Log("При отображении превью для видео произошла ошибка - " + ex.Message);
            }
            
        }

        private string RenameVideo(string fullname)
        {
            var name = "TgFileName.mp4".GetNextName(FileSystem.VideoPath);

            File.Copy(fullname, FileSystem.VideoPath + name);
            File.Delete(fullname);
            File.Delete(fullname + "-thumbnail.png");
            return FileSystem.VideoPath + name;
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

        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string res = string.Empty, folder;
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            if(openFileDialog.FileName.Contains(FileSystem.ImagePath) || openFileDialog.FileName.Contains(FileSystem.ThumnailPath) || openFileDialog.FileName.Contains(FileSystem.VideoPath))
            {
                System.Windows.MessageBox.Show("Вы пытаетесь добавить файл из папки приложения, выберите другой.");
                return;
            }

            if (!isVideoLoaded)
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                res = ".png";
                folder = FileSystem.ImagePath;
            }
            else
            {
                openFileDialog.Filter = "Video Files|*.mp4";
                res = ".mp4";
                folder = FileSystem.VideoPath;
            }
            var name = Path.Combine(folder + ("TgFileName" + res).GetNextName(folder));
           
            
            File.Copy(openFileDialog.FileName, name);
            if (isVideoLoaded)
            {
                FileSystem.CreateVideo(name);
                VideoGrid.Children.Clear();
                LoadVideos();
            }
            else
            {
                ImageGrid.Children.Clear();
                LoadImages();
            }
        }
    }
}
