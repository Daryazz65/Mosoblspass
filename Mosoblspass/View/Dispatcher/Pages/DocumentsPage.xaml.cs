using Mosoblspass.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mosoblspass.View.Dispatcher.Pages
{
    /// <summary>
    /// Логика взаимодействия для DocumentsPage.xaml
    /// </summary>
    public partial class DocumentsPage : Page
    {
        private static MosoblpoghspasEntities _context = App.GetContext();
        public DocumentsPage()
        {
            InitializeComponent();
            LoadDistricts();
        }
        private void LoadDistricts()
        {
            DocumentsStackPanel.Children.Clear();
            var photo = _context.Photos.FirstOrDefault(p => p.Name == "раменское");
            if (photo == null)
                return;
            var addressIds = _context.FireDispatchSchedules
                .Where(fds => fds.IdPhoto == photo.Id)
                .Select(fds => fds.IdAddress)
                .Distinct()
                .ToList();
            var addresses = _context.Addresses
                .Where(a => addressIds.Contains(a.Id))
                .ToList();
            string districtName = $"Раменское ({addresses.Count} адресов)";
            AddDistrictButton(districtName, photo.Name);
        }
        private void AddDistrictButton(string districtName, string imagePath)
        {
            var btn = new Button
            {
                Margin = new Thickness(0, 0, 0, 10),
                Width = 900,
                FontSize = 16
            };
            btn.Content = new TextBlock
            {
                Text = districtName,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            btn.Click += (s, e) =>
            {
                NavigationService?.Navigate(new DocumentImagePage(imagePath, districtName));
            };
            DocumentsStackPanel.Children.Add(btn);
        }
    }
}