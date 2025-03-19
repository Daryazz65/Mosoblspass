using Mosoblspass.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Mosoblspass
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static MosoblpoghspasEntities _context;
        public static MosoblpoghspasEntities GetContext()
        {
            if (_context == null)
            {
                _context = new MosoblpoghspasEntities();
            }
            return _context;
        }
    }
}
