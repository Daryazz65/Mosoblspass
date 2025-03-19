using Mosoblspass.AppData;
using Mosoblspass.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace Mosoblspass.View.Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        private static MosoblpoghspasEntities _context = App.GetContext();
        private int _selectedUserId;
        private string connectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        public ChangePasswordPage()
        {
            InitializeComponent();
            LoadUsers();
            UserDG.ItemsSource = _context.Users.ToList();
            AddRoleCmb.ItemsSource = _context.Roles.ToList();
            EditRoleCmb.ItemsSource = _context.Roles.ToList();
            AddRoleCmb.DisplayMemberPath = "Name";
            EditRoleCmb.DisplayMemberPath = "Name";
        }
        private void GetUsers()
        {
            UserDG.ItemsSource = _context.Users.ToList();
        }
        private void UserDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserDG.SelectedItem is User selectedUser)
            {
                txtFullName.Text = selectedUser.Name;
                EditRoleCmb.SelectedItem = selectedUser.Role;
                txtLogin.Text = selectedUser.Login;
                txtPassword.Text = selectedUser.Password;
                _selectedUserId = selectedUser.Id;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var productToDelete = (sender as FrameworkElement).DataContext as User;
            _context.Users.Remove(productToDelete);
            _context.SaveChanges();
            GetUsers();
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AddNameTbl.Text) && !string.IsNullOrEmpty(AddLoginBtn.Text)
                && !string.IsNullOrEmpty(AddPasswordBtn.Text) && AddRoleCmb.SelectedItem != null)
            {
                User newUser = new User()
                {
                    Name = AddNameTbl.Text,
                    Role = AddRoleCmb.SelectedItem as Role,
                    Login = AddLoginBtn.Text,
                    Password = AddPasswordBtn.Text
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
                GetUsers();
                MessageBoxHelper.Information("Сотрудник добавлен.");
            }
            else
            {
                MessageBoxHelper.Error("Не все данные были введены!");
            }
        }

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Выберите пользователя для редактирования.");
                return;
            }
            string newName = txtFullName.Text;
            string newRole = EditRoleCmb.SelectedItem?.ToString();
            string newLogin = txtLogin.Text;
            string newPassword = txtPassword.Text;
            int newRoleId = (int)EditRoleCmb.SelectedValue;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE [User] SET Name = @Name, IdRole = @Role, " +
                    "Login = @Login, Password = @Password WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", newName);
                    cmd.Parameters.AddWithValue("@Role", newRoleId);
                    cmd.Parameters.AddWithValue("@Login", newLogin);
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    cmd.Parameters.AddWithValue("@Id", _selectedUserId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные обновлены!");
                        LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка обновления.");
                    }
                }
            }
        }
        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM [User]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                UserDG.ItemsSource = dt.DefaultView;
            }
        }
    }
}
