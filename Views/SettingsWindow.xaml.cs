using OverlayImageForWindows.Models.Data;
using OverlayImageForWindows.Models;
using System;
using System.Diagnostics;
using System.Windows;
using OverlayImageForWindows.Models.TG;
using Telegram.Bot;
using System.IO;
using System.ComponentModel;
using Newtonsoft.Json;

namespace OverlayImageForWindows.Views
{
    public partial class SettingsWindow : Window
    {
        public Action<float> OnOpacityChanged;
        public SettingsWindow()
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
                Process.Start(new ProcessStartInfo("cmd", "/c start https://t.me/raw_info_bot") { CreateNoWindow = true});
            };

            TokenText.MouseLeftButtonDown += delegate
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start https://t.me/BotFather") { CreateNoWindow = true });
            };

            if (FileSystem.info == null) return;
            IdInput.Text = FileSystem.info.TelegramID.ToString();
            TokenInput.Text = FileSystem.info.Token;
            AcceptOtherFiles.IsChecked = FileSystem.info.AcceptTPUFiles;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (IdInput.Text == string.Empty || TokenInput.Text == string.Empty)
            {
                new Log("Не все поля заполнены, изменения не внесены.");
                base.OnClosing(e);
                return;
            }
            if (!long.TryParse(IdInput.Text, out var result))
            {
                new Log("Неверный телеграм айди, изменения не внесены");
                base.OnClosing(e);
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
            base.OnClosing(e);
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
    }
}
