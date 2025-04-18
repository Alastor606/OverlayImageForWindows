using System;
using System.Windows;
using System.ComponentModel;

namespace OverlayImageForWindows.Views
{
    public partial class SettingsWindow : Window
    {
        public Action<float> OnOpacityChanged;
        public Action<float> OnVolumeChanged;
        private SettingsMainPage _settingsPage;

        public SettingsWindow()
        {
            InitializeComponent();
            _settingsPage = new SettingsMainPage();
            _settingsPage.OnOpacityChanged += e =>
            {
                OnOpacityChanged?.Invoke(e);
            };
            _settingsPage.OnVolumeChanged += e =>
            {
                OnVolumeChanged?.Invoke(e);
            };
            MainFrame.Navigate(_settingsPage);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _settingsPage.OnClosing(e);
            base.OnClosing(e);
        }
    }
}
