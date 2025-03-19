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
    }
}
