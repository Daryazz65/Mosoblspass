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

            // Находим фото "раменское"
            var photo = _context.Photos.FirstOrDefault(p => p.Name == "раменское");
            if (photo == null)
                return;

            // Получаем все адреса, связанные с этим фото через FireDispatchSchedule
            var addressIds = _context.FireDispatchSchedules
                .Where(fds => fds.IdPhoto == photo.Id)
                .Select(fds => fds.IdAddress)
                .Distinct()
                .ToList();

            var addresses = _context.Addresses
                .Where(a => addressIds.Contains(a.Id))
                .ToList();

            // Кнопка для района "Раменское" с количеством адресов
            string districtName = $"Раменское ({addresses.Count} адресов)";
            AddDistrictButton(districtName, photo.Name);
        }
        private void AddDistrictButton(string districtName, string imagePath)
        {
            var btn = new Button
            {
                Margin = new Thickness(0, 0, 0, 10),
                Height = 40,
                FontSize = 16
            };
            // Используем TextBlock с белым текстом внутри кнопки
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