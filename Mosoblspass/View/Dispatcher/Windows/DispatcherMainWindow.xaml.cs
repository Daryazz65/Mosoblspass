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
using System.Windows.Shapes;

namespace Mosoblspass.View.Dispatcher.Windows
{
    /// <summary>
    /// Логика взаимодействия для DispatcherMainWindow.xaml
    /// </summary>
    public partial class DispatcherMainWindow : Window
    {
        private static MosoblpoghspasEntities _context = App.GetContext();
        public DispatcherMainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                var address = _context.Addresses.FirstOrDefault(a => a.Name.Contains(searchText));
                if (address != null)
                {
                    var photo = _context.Photos.FirstOrDefault(p => p.Id == address.Id);
                    if (photo != null)
                    {
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
                        MessageBox.Show("Фото не найдено для данного адреса.");
                    }
                }
                else
                {
                    MessageBox.Show("Адрес не найден.");
                }
            }
            else
            {
                MessageBox.Show("Введите адрес для поиска.");
            }
        }
    }
}
