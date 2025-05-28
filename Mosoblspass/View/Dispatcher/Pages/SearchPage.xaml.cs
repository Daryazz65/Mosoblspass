using Mosoblspass.AppData;
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
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        private static MosoblpoghspasEntities _context = App.GetContext();
        public SearchPage()
        {
            InitializeComponent();
            SearchHistory.Add(SearchTextBox.Text);
        }
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            SearchHistory.Add(SearchTextBox.Text);
            if (!string.IsNullOrEmpty(searchText))
            {
                var address = _context.Addresses.FirstOrDefault(a => a.Name.Contains(searchText));
                if (address != null)
                {
                    var schedule = _context.FireDispatchSchedules.FirstOrDefault(s => s.IdAddress == address.Id);
                    if (schedule != null && schedule.Photo != null)
                    {
                        var photo = schedule.Photo;
                        BitmapImage bitmap = new BitmapImage();
                        using (var stream = new System.IO.MemoryStream(photo.PhotoBinary))
                        {
                            bitmap.BeginInit();
                            bitmap.StreamSource = stream;
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();
                        }
                        ResultImage.Source = bitmap;
                    }
                    else
                    {
                        MessageBoxHelper.Error("Для данного адреса документ не найден.");
                    }
                }
                else
                {
                    MessageBoxHelper.Error("Адрес не найден в базе данных. Проверьте правильность ввода адреса.");
                }

            }
            else
            {
                MessageBoxHelper.Error("Пожалуйста, введите адрес для поиска.");
            }
        }
    }
}
