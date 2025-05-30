using Mosoblspass.AppData;
using Mosoblspass.View.Admin.Pages;
using Mosoblspass.View.Dispatcher.Pages;
using Mosoblspass.View.Login.Windows;
using System.Windows;

namespace Mosoblspass.View.Admin.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            CrudUserPage crudUserPage = new CrudUserPage();
            MainFrame.Navigate(crudUserPage);
            FrameHelper.selectedFrame = MainFrame;
        }
        private void GoOutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profilePage = new ProfilePage();
            MainFrame.Navigate(profilePage);
        }
        private void UserBtn_Click(object sender, RoutedEventArgs e)
        {
            CrudUserPage crudUserPage = new CrudUserPage();
            MainFrame.Navigate(crudUserPage);
        }
        private void DocumsBtn_Click(object sender, RoutedEventArgs e)
        {
            CrudDocumPage crudDocumPage = new CrudDocumPage();
            MainFrame.Navigate(crudDocumPage);
        }
    }
}