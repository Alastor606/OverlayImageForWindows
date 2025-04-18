using OverlayImageForWindows.Models;
using OverlayImageForWindows.Models.Data;
using OverlayImageForWindows.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace OverlayImageForWindows
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;

        private bool isMediaOpened = false, isSettingsOpened = false;
        public bool isVideo { get; private set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            MainImage.MouseLeftButtonDown += delegate
            {
                this.DragMove();
            };
            MainImage.MouseRightButtonDown += delegate
            {
                if(Settings.Visibility == Visibility.Visible)
                {
                    Settings.Visibility = Visibility.Hidden;
                    return;
                }
                Settings.Visibility = Visibility.Visible;
            };
            Settings.MouseRightButtonDown += delegate
            {
                if (Settings.Visibility == Visibility.Visible)
                {
                    Settings.Visibility = Visibility.Hidden;
                    return;
                }
                Settings.Visibility = Visibility.Visible;
            };
            Loaded += (s, e) =>
            {
                SetWindowPos(new System.Windows.Interop.WindowInteropHelper(this).Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            };

            MainVideo.MouseLeftButtonDown += delegate
            {
                this.DragMove();
            };
            MainVideo.MouseRightButtonDown += delegate
            {
                if (Settings.Visibility == Visibility.Visible)
                {
                    Settings.Visibility = Visibility.Hidden;
                    return;
                }
                Settings.Visibility = Visibility.Visible;
            };
            MainVideo.MediaFailed += MediaElement_MediaFailed;
            RenderOptions.SetBitmapScalingMode(MainVideo, BitmapScalingMode.HighQuality);
            FileSystem.Init(this);
            isVideo = FileSystem.config.IsVideo;
            MainVideo.Opacity = FileSystem.config.ImageOpacity;
            MainImage.Opacity = FileSystem.config.ImageOpacity;
        }

        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e) =>
            System.Windows.MessageBox.Show($"Ошибка воспроизведения: {e.ErrorException.Message}\n Название видео - {MainVideo.Source.LocalPath.GetFileName()}");
        

        private void DroppedImage_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))e.Effects = System.Windows.DragDropEffects.Copy;
            else e.Effects = System.Windows.DragDropEffects.None;
            
            e.Handled = true;
        }

        private void DroppedImage_DragOver(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop)) e.Effects = System.Windows.DragDropEffects.Copy;
            else e.Effects = System.Windows.DragDropEffects.None;
            
            e.Handled = true;
        }

        private void DroppedImage_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    var path = files[0];
                    if (path.Contains(".mp4"))
                    {
                        MainVideo.Visibility = Visibility.Visible;
                        MainImage.Visibility = Visibility.Hidden;
                        MainVideo.Source = new Uri(FileSystem.SaveVideo(path));
                        MainVideo.Play();
                    }
                    else
                    {
                        MainImage.Visibility = Visibility.Visible;
                        BitmapImage bitmap = new BitmapImage(new Uri(path));
                        MainImage.Source = bitmap;
                        FileSystem.SaveImage(files[0]);
                        MainVideo.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var screens = Screen.AllScreens;

            if (screens.Length > 1)
            {
                var secondScreen = screens[0].WorkingArea;

                this.Left = secondScreen.Left;
                this.Top = secondScreen.Top;
                this.Width = secondScreen.Width;
                this.Height = secondScreen.Height;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
            this.WindowState = WindowState.Normal;
            this.Width = FileSystem.config.ScreenSize.X;
            this.Height = FileSystem.config.ScreenSize.Y;
            if (FileSystem.config.ImagePath != string.Empty && !FileSystem.config.IsVideo)
            {
                MainImage.SetImage(FileSystem.config.ImagePath);
            }
            else
            {
                MainImage.Visibility = Visibility.Hidden;
                MainVideo.Visibility = Visibility.Visible;
                MainVideo.Source = new Uri(FileSystem.VideoPath + FileSystem.config.ImagePath);
                MainVideo.Play();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) =>
            this.Close();

#region thumbnails
        // Угловые Thumb
        private void Thumb_DragDelta_TopLeft(object sender, DragDeltaEventArgs e)
        {
            this.Width = Math.Max(this.Width - e.HorizontalChange, 100);
            this.Height = Math.Max(this.Height - e.VerticalChange, 100);
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }

        private void Thumb_DragDelta_TopRight(object sender, DragDeltaEventArgs e)
        {
            this.Width = Math.Max(this.Width + e.HorizontalChange, 100);
            this.Height = Math.Max(this.Height - e.VerticalChange, 100);
            this.Top += e.VerticalChange;
        }

        private void Thumb_DragDelta_BottomLeft(object sender, DragDeltaEventArgs e)
        {
            this.Width = Math.Max(this.Width - e.HorizontalChange, 100);
            this.Height = Math.Max(this.Height + e.VerticalChange, 100);
            this.Left += e.HorizontalChange;
        }

        private void Thumb_DragDelta_BottomRight(object sender, DragDeltaEventArgs e)
        {
            this.Width = Math.Max(this.Width + e.HorizontalChange, 100);
            this.Height = Math.Max(this.Height + e.VerticalChange, 100);
        }

        // Боковые Thumb
        private void Thumb_DragDelta_Left(object sender, DragDeltaEventArgs e)
        {
            this.Width = Math.Max(this.Width - e.HorizontalChange, 100);
            this.Left += e.HorizontalChange;
        }

        private void Thumb_DragDelta_Right(object sender, DragDeltaEventArgs e)
        {
            this.Width = Math.Max(this.Width + e.HorizontalChange, 100);
        }

        private void Thumb_DragDelta_Top(object sender, DragDeltaEventArgs e)
        {
            this.Height = Math.Max(this.Height - e.VerticalChange, 100);
            this.Top += e.VerticalChange;
        }

        private void Thumb_DragDelta_Bottom(object sender, DragDeltaEventArgs e)
        {
            this.Height = Math.Max(this.Height + e.VerticalChange, 100);
        }
        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            FileSystem.Save(this);
            base.OnClosing(e);
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if(MainImage.Visibility == Visibility.Visible)
            {
                try
                {
                    using (System.Drawing.Image image = System.Drawing.Image.FromFile(FileSystem.ImagePath + (MainImage.Source as BitmapImage).UriSource?.AbsolutePath.Split('/').Last()))
                    {
                        System.Windows.Forms.Clipboard.SetImage(image);
                    }
                }
                catch (Exception ex)
                {
                    new Log($"При попытке копирования произошла ошибка ({ex.Message}), возможно название файла написанно на русском, исправьте это.");
                }
            }
            else
            {
                var name = ((MainVideo.Source).ToString().GetFileName());
                System.Windows.Forms.Clipboard.SetImage(System.Drawing.Image.FromFile(FileSystem.ThumnailPath + name + "-thumbnail.png"));
            }
        }

        private void ChangeSize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }


        private void PickMedia_Click(object sender, RoutedEventArgs e)
        {
            if(isMediaOpened)return;
            isMediaOpened = true;
            var window = new GetMediaWindow();
            window.Show();
            window.Closed += delegate
            {
                isMediaOpened = false;
            };
            window.OnMediaPicked += (s,isVideo) =>
            {
                var fileName = s.Split('_')[1];
                this.isVideo = isVideo;
                if (isVideo)
                {
                    MainImage.Visibility = Visibility.Hidden;
                    MainVideo.Visibility = Visibility.Visible;
                    MainVideo.Source = new Uri(FileSystem.VideoPath + fileName + ".mp4");
                    MainVideo.Play();
                }
                else
                {
                    MainImage.Visibility = Visibility.Visible;
                    MainVideo.Visibility = Visibility.Hidden;

                    var name = fileName.GetImageType(FileSystem.ImagePath);
                    MainImage.SetImage(name);
                }
            };
        }

        private void MediaEnded(object sender, RoutedEventArgs e)
        {
            MainVideo.Position = TimeSpan.Zero;
            MainVideo.Play();
        }

        private void MegaSettings_Click(object sender, RoutedEventArgs e)
        {
            if (isSettingsOpened) return;
            isSettingsOpened = true;
            var window = new SettingsWindow();
            window.Show();
            window.OnOpacityChanged += x =>
            {
                MainVideo.Opacity = x;
                MainImage.Opacity = x;
            };
            window.OnVolumeChanged += x =>
            {
                MainVideo.Volume = x;
            };
            window.Closing += delegate
            {
                isSettingsOpened = false;
            };
        }
    }
}
