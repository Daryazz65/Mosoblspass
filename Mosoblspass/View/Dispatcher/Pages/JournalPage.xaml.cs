using Mosoblspass.AppData;
using Mosoblspass.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Mosoblspass.View.Dispatcher.Pages
{
    public partial class JournalPage : Page
    {
        private static MosoblpoghspasEntities _context = App.GetContext();

        public JournalPage()
        {
            InitializeComponent();
            HistoryListBox.ItemsSource = SearchHistory.History;
        }

        private void HistoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedAddress = HistoryListBox.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedAddress))
                return;

            var address = _context.Addresses.FirstOrDefault(a => a.Name.Contains(selectedAddress));
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
                    // Покажите фото, например, в новом окне или на странице
                    var window = new Window
                    {
                        Title = "Расписание выезда",
                        Content = new Image { Source = bitmap, Stretch = System.Windows.Media.Stretch.Uniform },
                        Width = 600,
                        Height = 400
                    };
                    window.ShowDialog();
                }
                else
                {
                    MessageBoxHelper.Error("Для данного адреса документ не найден.");
                }
            }
            else
            {
                MessageBoxHelper.Error("Адрес не найден в базе данных.");
            }
        }
    }
}