using Mosoblspass.Model;
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
using System.IO; // Add this namespace at the top of the file


namespace Mosoblspass.View.Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для CrudDocumPage.xaml
    /// </summary>
    public partial class CrudDocumPage : Page
    {
        private MosoblpoghspasEntities _context = new MosoblpoghspasEntities();
        private List<Address> _addresses;
        private List<Photo> _documents;
        public CrudDocumPage()
        {
            InitializeComponent();
            LoadAddresses();
            LoadDocuments();
        }

        private void AddAddress_Click(object sender, RoutedEventArgs e)
        {
            var address = new Address { Name = AddressTextBox.Text };
            _context.Addresses.Add(address);
            _context.SaveChanges();
            AddressComboBox.ItemsSource = _context.Addresses.ToList();
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

                // Обновить список
                LoadSchedules();
            }
        }
        private void LoadSchedules()
        {
            ScheduleGrid.ItemsSource = _context.FireDispatchSchedules
                .Include("Address")
                .Include("Photo")
                .ToList();
        }
        private void LoadAddresses()
        {
            using (var db = new MosoblpoghspasEntities())
            {
                _addresses = new List<Address>(db.Addresses);
                AddressComboBox.ItemsSource = _addresses;
            }
        }

        private void LoadDocuments()
        {
            using (var db = new MosoblpoghspasEntities())
            {
                _documents = db.Photos
                    .Include("FireDispatchSchedules.Address")
                    .ToList();
                ScheduleGrid.ItemsSource = _documents;
            }
        }


    }
}
