using Mosoblspass.AppData;
using Mosoblspass.View.Login.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.Win32;
using Mosoblspass.Model;
using System.Linq;


namespace Mosoblspass.View.Dispatcher.Pages
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadProfileData();
        }

        private void LoadProfileData()
        {
            if (CurrentUser.User != null)
            {
                NameTextBlock.Text = CurrentUser.User.Name;
                LoginTextBlock.Text = CurrentUser.User.Login;
                RoleTextBlock.Text = CurrentUser.User.Role?.Name;
                EmailTextBlock.Text = CurrentUser.User.Email ?? "Не указано";
                PhoneTextBlock.Text = CurrentUser.User.Phone ?? "Не указано";
                AddressTextBlock.Text = CurrentUser.User.Address ?? "Не указано";

                if (CurrentUser.User.Photo != null && CurrentUser.User.Photo.Length > 0)
                {
                    PhotoImg.Source = ByteArrayToImage(CurrentUser.User.Photo);
                }
                else
                {
                    PhotoImg.Source = null; // или изображение по умолчанию
                }
            }
            else
            {
                NameTextBlock.Text = "Неизвестно";
                LoginTextBlock.Text = "Неизвестно";
                RoleTextBlock.Text = "Неизвестно";
                EmailTextBlock.Text = "Неизвестно";
                PhoneTextBlock.Text = "Неизвестно";
                AddressTextBlock.Text = "Неизвестно";
                PhotoImg.Source = null;
            }
        }

        private BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Изображения (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                byte[] photoBytes = File.ReadAllBytes(openFileDialog.FileName);
                CurrentUser.User.Photo = photoBytes;

                // Сохраняем изменения в БД
                using (var context = new MosoblpoghspasEntities())
                {
                    var user = context.Users.AsQueryable().FirstOrDefault(u => u.Id == CurrentUser.User.Id);
                    if (user != null)
                    {
                        user.Photo = photoBytes;
                        context.SaveChanges();
                    }
                }
                LoadProfileData();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.User.Photo = null;

            // Сохраняем изменения в БД
            using (var context = new MosoblpoghspasEntities())
            {
                var user = context.Users.AsQueryable().FirstOrDefault(u => u.Id == CurrentUser.User.Id);
                if (user != null)
                {
                    user.Photo = null;
                    context.SaveChanges();
                }
            }
            LoadProfileData();
        }

        private void GoOutBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.User = null;
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Close();
        }
    }
}
