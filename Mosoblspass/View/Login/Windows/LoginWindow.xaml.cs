using Mosoblspass.AppData;
using Mosoblspass.Model;
using Mosoblspass.View.Admin.Windows;
using Mosoblspass.View.Dispatcher.Windows;
using System.Windows;
namespace Mosoblspass.View.Login.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool _isCaptchaVerified = false;
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!_isCaptchaVerified)
            {
                MessageBoxHelper.Information("Пожалуйста, пройдите капчу перед входом.");
                return;
            }
            if (string.IsNullOrWhiteSpace(LoginTb.Text) || string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                MessageBoxHelper.Information("Пожалуйста, введите логин и пароль.");
                return;
            }
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
        private void HL2_Click(object sender, RoutedEventArgs e)
        {
            CaptchaWindow captchaWindow = new CaptchaWindow();
            if (captchaWindow.ShowDialog() == true && captchaWindow.IsVerified)
            {
                _isCaptchaVerified = true;
                MessageBoxHelper.Information("Капча пройдена успешно.");
            }
            else
            {
                _isCaptchaVerified = false;
                MessageBoxHelper.Information("Капча не пройдена.");
            }
        }
    }
}