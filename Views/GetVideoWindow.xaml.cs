using OverlayImageForWindows.Models;
using OverlayImageForWindows.Models.Data;
using System;
using System.IO;
using System.Windows;



namespace OverlayImageForWindows.Views
{
    public partial class GetVideoWindow : Window
    {
        public Action<string> OnPickVideo;
        public GetVideoWindow()
        {
            InitializeComponent();
            VideoView.ItemsSource = new DirectoryInfo(FileSystem.VideoPath).GetFiles().ToVideos();
            VideoView.MouseDoubleClick += delegate
            {
                OnPickVideo?.Invoke((VideoView.SelectedItem as OverlayVideo).FullPath);
                this.Close();
            };
        }

        private void VideoView_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            int currentIndex = VideoView.SelectedIndex;

            if (e.Delta > 0)
            {
                if (currentIndex > 0)
                {
                    VideoView.SelectedIndex = currentIndex - 1;
                    VideoView.ScrollIntoView(VideoView.SelectedItem);
                }
            }
            else if (e.Delta < 0)
            {
                if (currentIndex < VideoView.Items.Count - 1)
                {
                    VideoView.SelectedIndex = currentIndex + 1;
                    VideoView.ScrollIntoView(VideoView.SelectedItem);
                }
            }

            e.Handled = true;
        }
    }
}
