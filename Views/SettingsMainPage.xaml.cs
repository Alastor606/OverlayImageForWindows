using OverlayImageForWindows.Models.TG;
using OverlayImageForWindows.Models;
using System.Diagnostics;
using System.Windows.Controls;
using System;
using Newtonsoft.Json;
using OverlayImageForWindows.Models.Data;
using System.ComponentModel;
using System.Windows;
using Telegram.Bot;
using System.IO;

namespace OverlayImageForWindows.Views
{
    public partial class SettingsMainPage : Page
    {
        public Action<float> OnOpacityChanged;
        public Action<float> OnVolumeChanged;

        public SettingsMainPage()
        {
            InitializeComponent();
            BotCheckButton.IsEnabled = Bot.IsConnected;
            AcceptOtherFiles.Checked += delegate
            {
                FileSystem.info.AcceptTPUFiles = (bool)AcceptOtherFiles.IsChecked;
            };
            ImageOpacity.Value = FileSystem.config.ImageOpacity;
            ImageOpacity.ValueChanged += delegate
            {
                OnOpacityChanged?.Invoke((float)ImageOpacity.Value);
                FileSystem.config.ImageOpacity = (float)ImageOpacity.Value;
            };

            IdText.MouseLeftButtonDown += delegate
            {
                Process.Start(new ProcessStartInfo("cmd", "/c start https://t.me/raw_info_bot") { CreateNoWindow = true });
            };

            TokenText.MouseLeftButtonDown += delegate
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start https://t.me/BotFather") { CreateNoWindow = true });
            };

            VideoVolume.Value = FileSystem.config.VideoVolume;
            VideoVolume.ValueChanged += delegate
            {
                OnVolumeChanged?.Invoke((float)VideoVolume.Value);
                FileSystem.config.VideoVolume = (float)VideoVolume.Value;
            };

            if (FileSystem.info == null) return;
            IdInput.Text = FileSystem.info.TelegramID.ToString();
            TokenInput.Text = FileSystem.info.Token;
            AcceptOtherFiles.IsChecked = FileSystem.info.AcceptTPUFiles;
        }

        public void OnClosing(CancelEventArgs e)
        {
            if (IdInput.Text == string.Empty || TokenInput.Text == string.Empty)
            {
                new Log("Не все поля заполнены, изменения не внесены.");
                return;
            }
            if (!long.TryParse(IdInput.Text, out var result))
            {
                new Log("Неверный телеграм айди, изменения не внесены");
                return;
            }
            FileSystem.info = new Models.Data.UserInfo()
            {
                TelegramID = long.Parse(IdInput.Text),
                Token = TokenInput.Text,
                AcceptTPUFiles = (bool)AcceptOtherFiles.IsChecked,
            };
            File.WriteAllText(FileSystem.DataFile, JsonConvert.SerializeObject(FileSystem.info));
            Bot.Init();
        }

        private void HistoryCheck_Click(object sender, RoutedEventArgs e) =>
            Process.Start("notepad.exe", FileSystem.LogPath);

        private async void BotCheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Bot.IsConnected)
            {
                MessageBox.Show("Бот не подключен");
                return;
            }
            await Bot.client.SendMessage(FileSystem.info.TelegramID, "Большая какашка");
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersPage());
        }
    }
}
