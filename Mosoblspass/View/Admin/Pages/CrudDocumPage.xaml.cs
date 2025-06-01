using Mosoblspass.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Threading.Tasks;

namespace Mosoblspass.View.Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для CrudDocumPage.xaml
    /// </summary>
    public partial class CrudDocumPage : Page
    {
        private static MosoblpoghspasEntities _context = App.GetContext();
        private List<Address> _addresses;
        public CrudDocumPage()
        {
            InitializeComponent();
            LoadSchedulesAsync();
            LoadAddressesAsync();
        }
        private async void AddAddress_Click(object sender, RoutedEventArgs e)
        {
            var address = new Address { Name = AddressTextBox.Text };
            _context.Addresses.Add(address);
            _context.SaveChanges();
            var addresses = await Task.Run(() => _context.Addresses.ToList());
            _addresses = addresses;
            AddressComboBox.ItemsSource = _addresses;
        }
        private string _selectedFilePath;
        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                _selectedFilePath = dialog.FileName;
                SelectedPhotoText.Text = System.IO.Path.GetFileName(_selectedFilePath);
            }
        }
        private void AddDocument_Click(object sender, RoutedEventArgs e)
        {
            if (AddressComboBox.SelectedItem is Address address && !string.IsNullOrEmpty(_selectedFilePath))
            {
                var photo = new Photo
                {
                    Name = System.IO.Path.GetFileName(_selectedFilePath),
                    PhotoBinary = File.ReadAllBytes(_selectedFilePath)
                };
                _context.Photos.Add(photo);
                _context.SaveChanges();
                var schedule = new FireDispatchSchedule
                {
                    IdAddress = address.Id,
                    IdPhoto = photo.Id
                };
                _context.FireDispatchSchedules.Add(schedule);
                _context.SaveChanges();
                LoadSchedulesAsync();
            }
        }
        private async void LoadSchedulesAsync()
        {
            ScheduleGrid.ItemsSource = null;
            var data = await Task.Run(() =>
                _context.FireDispatchSchedules
                    .Include("Address")
                    .Include("Photo")
                    .ToList()
            );
            ScheduleGrid.ItemsSource = data;
        }
        private async void LoadAddressesAsync()
        {
            List<Address> addresses = new List<Address>(); 
            await Task.Run(() =>
            {
                using (var context = new MosoblpoghspasEntities())
                {
                    addresses = context.Addresses.ToList();
                }
            });
            _addresses = addresses;
            AddressComboBox.ItemsSource = _addresses;
        }



        private void AddressTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AddressTextBox.Text == "Введите название адреса")
            {
                AddressTextBox.Text = "";
            }
        }

        private void AddressTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (AddressTextBox.Text == "")
            {
                AddressTextBox.Text = "Введите название адреса";
            }
        }

        private void DeleteDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSchedule = ScheduleGrid.SelectedItem as FireDispatchSchedule;
            if (selectedSchedule != null)
            {
                var photo = _context.Photos.FirstOrDefault(p => p.Id == selectedSchedule.IdPhoto);
                if (photo != null)
                    _context.Photos.Remove(photo);

                _context.FireDispatchSchedules.Remove(selectedSchedule);
                _context.SaveChanges();
                LoadSchedulesAsync();
            }
        }

    }
}