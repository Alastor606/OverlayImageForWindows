using OverlayImageForWindows.Models;
using System;
using System.IO;
using System.Windows;


namespace OverlayImageForWindows.Views
{
    public partial class GetImageWindow : Window
    {
        public Action<string> OnPickImage;
        public GetImageWindow()
        {
            InitializeComponent();
            ImageView.ItemsSource = new DirectoryInfo(FileSystem.ImagePath).GetFiles();
            ImageView.MouseDoubleClick += delegate
            {
                OnPickImage?.Invoke((ImageView.SelectedItem as FileInfo).FullName);
                this.Close();
            };
        }
    }
}
