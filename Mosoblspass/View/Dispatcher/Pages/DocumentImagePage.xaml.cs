using Mosoblspass.Model;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace Mosoblspass.View.Dispatcher.Pages
{
    public partial class DocumentImagePage : Page
    {
        private static MosoblpoghspasEntities _context = App.GetContext();

        public DocumentImagePage(string imagePath, string title)
        {
            InitializeComponent();
            TitleTextBlock.Text = title; // Use the 'title' parameter instead of 'districtName'

            // Получаем фото из БД по имени
            var photo = _context.Photos.FirstOrDefault(p => p.Name == imagePath); // Use 'imagePath' parameter instead of 'imageName'
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
                MessageBox.Show("Изображение не найдено в базе данных.");
            }
        }
    }
}