using OverlayImageForWindows.Models;
using OverlayImageForWindows.Models.Data;
using OverlayImageForWindows.Models.Data.TelegramData;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OverlayImageForWindows.Views
{
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            UsersView.ItemsSource = FileSystem.users.ToInfo();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var name = (sender as CheckBox).Tag.ToString().Split('_')[1];
            var user = FileSystem.users.FirstOrDefault(x => x.TelegramId == long.Parse(name));
            user.InBlackList =(bool) (sender as CheckBox).IsChecked;
            new Log(user.InBlackList + " - " + user.TelegramId);
            FileSystem.SaveUsers();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
