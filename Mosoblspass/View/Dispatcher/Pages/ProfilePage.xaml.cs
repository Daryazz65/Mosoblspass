using Mosoblspass.AppData;
using Mosoblspass.View.Login.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mosoblspass.View.Dispatcher.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            if (CurrentUser.User != null)
            {
                NameTextBlock.Text = CurrentUser.User.Name;
                LoginTextBlock.Text = CurrentUser.User.Login;
                RoleTextBlock.Text = CurrentUser.User.Role?.Name;
            }
        }

        private void GoOutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show(); // Open the LoginWindow
            Window.GetWindow(this)?.Close(); // Close the current window hosting the ProfilePage
        }

    }
}