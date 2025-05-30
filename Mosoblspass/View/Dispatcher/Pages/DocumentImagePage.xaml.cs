using Mosoblspass.AppData;
using Mosoblspass.Model;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Mosoblspass.View.Dispatcher.Pages
{
    public partial class DocumentImagePage : Page
    {
        private static MosoblpoghspasEntities _context = App.GetContext();
        public DocumentImagePage(string imagePath, string title)
        {
            InitializeComponent();
            TitleTextBlock.Text = title; 
            var photo = _context.Photos.FirstOrDefault(p => p.Name == imagePath); 
            if (photo != null && photo.PhotoBinary != null)
            {
                using (var ms = new MemoryStream(photo.PhotoBinary))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    DocumentImage.Source = bitmap;
                }
            }
            else
            {
                MessageBoxHelper.Error("Изображение не найдено в базе данных.");
            }
        }
    }
}