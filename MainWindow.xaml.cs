using OverlayImageForWindows.Models;
using OverlayImageForWindows.Models.Data;
using OverlayImageForWindows.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
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
            Loaded += (s, e) =>
            {
                SetWindowPos(new System.Windows.Interop.WindowInteropHelper(this).Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            };
            FileSystem.Init(this);
        }

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
                    BitmapImage bitmap = new BitmapImage(new Uri(files[0]));
                    MainImage.Source = bitmap;
                    FileSystem.SaveImage(files[0]);
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
            this.Width = FileSystem.config.ScreenSize.X;
            this.Height = FileSystem.config.ScreenSize.Y;
            if (FileSystem.config.ImagePath != string.Empty)
            {
                MainImage.SetImage(FileSystem.config.ImagePath);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) =>
            this.Close();
        

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

        protected override void OnClosing(CancelEventArgs e)
        {
            FileSystem.Save(this,MainImage.Source.ToString().GetFileName());
            base.OnClosing(e);
        }

        private void PickImage_Click(object sender, RoutedEventArgs e)
        {
            var window = new GetImageWindow();
            window.Show();
            window.OnPickImage += (s) => MainImage.SetImage(s.GetFileName2());
        }

        private void HistoryCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("notepad.exe", FileSystem.LogPath);
            }
            catch (Exception ex)
            {
                new Log($"ошибка истории - {ex.Message}");
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            using (Image image = Image.FromFile((MainImage.Source as BitmapImage).UriSource?.AbsolutePath))
            {
                System.Windows.Forms.Clipboard.SetImage(image);
            }
        }

        private void ChangeSize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
    }
}
