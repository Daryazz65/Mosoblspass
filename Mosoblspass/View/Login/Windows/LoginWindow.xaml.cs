using Mosoblspass.AppData;
using Mosoblspass.Model;
using Mosoblspass.View.Admin.Windows;
using Mosoblspass.View.Dispatcher.Windows;
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
using System.Windows.Shapes;

namespace Mosoblspass.View.Login.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private static MosoblpoghspasEntities _context = App.GetContext();
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthHelper.Authenticate(LoginTb.Text, PasswordPb.Password);
            if (AuthHelper.selectedUser != null)
            {
                App.CurrentUser = AuthHelper.selectedUser;
                if (AuthHelper.selectedUser.IdRole == 1)
                {
                    DispatcherMainWindow dispatcherMainWindow = new DispatcherMainWindow();
                    dispatcherMainWindow.Show();
                    CurrentUser.User = AuthHelper.selectedUser; 

                }
                else
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    CurrentUser.User = AuthHelper.selectedUser;

                }
                Close();
            }
        }
        private void HL1_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxHelper.Information("Пожалуйста, обратитесь к системному администратору для восстановления пароля.");
        }

        private void CaptchaBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HL2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}