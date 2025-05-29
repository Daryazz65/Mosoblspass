using Mosoblspass.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using Mosoblspass.AppData;

namespace Mosoblspass.View.Admin.Pages
{
    public partial class CrudUserPage : Page
    {
        private static MosoblpoghspasEntities _context = App.GetContext();
        private User selectedUser; 

        public CrudUserPage()
        {
            InitializeComponent();
            UserGrid.ItemsSource = _context.Users.ToList();
            RoleBox.ItemsSource = _context.Roles.ToList();
            RoleBox.DisplayMemberPath = "Name";
            RoleBox.SelectedIndex = 0;
            LoadData();
        }
        private void LoadData()
        {
            UserGrid.ItemsSource = _context.Users.ToList();
            RoleBox.ItemsSource = _context.Roles.ToList();
            RoleBox.DisplayMemberPath = "Name";
        }
        private void ClearInputs()
        {
            NameBox.Text = "";
            LoginBox.Text = "";
            PasswordBox.Password = "";
            RoleBox.SelectedIndex = -1;
            EmailBox.Text = "";
            PhoneBox.Text = "";
            AddressBox.Text = "";
            selectedUser = null;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                string.IsNullOrWhiteSpace(LoginBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                RoleBox.SelectedItem == null)
            {
                MessageBox.Show("Заполните все обязательные поля (ФИО, Логин, Пароль, Роль).");
                return;
            }

            var newUser = new User
            {
                Name = NameBox.Text,
                Login = LoginBox.Text,
                Password = PasswordBox.Password,
                IdRole = (int)RoleBox.SelectedValue,
                Email = EmailBox.Text,
                Phone = PhoneBox.Text,
                Address = AddressBox.Text
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            LoadData();
        }

        private void UserGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserGrid.SelectedItem is User user)
            {
                selectedUser = user;
                NameBox.Text = user.Name;
                LoginBox.Text = user.Login;
                PasswordBox.Password = user.Password;
                RoleBox.SelectedValue = user.IdRole;
                EmailBox.Text = user.Email;
                PhoneBox.Text = user.Phone;
                AddressBox.Text = user.Address;
            }
        }


        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для редактирования.");
                return; 
            }

            selectedUser.Name = NameBox.Text;
            selectedUser.Login = LoginBox.Text;
            selectedUser.Password = PasswordBox.Password;
            selectedUser.IdRole = (int)RoleBox.SelectedValue;
            selectedUser.Email = EmailBox.Text;
            selectedUser.Phone = PhoneBox.Text;
            selectedUser.Address = AddressBox.Text;

            _context.SaveChanges();
            LoadData();

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button)?.Tag as User ?? selectedUser;
            if (user == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.");
                return;
            }

            if (MessageBox.Show("Удалить выбранного пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                LoadData();

            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void RoleBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
