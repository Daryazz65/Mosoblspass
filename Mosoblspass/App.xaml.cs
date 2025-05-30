﻿using Mosoblspass.Model;
using System.Windows;

namespace Mosoblspass
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }
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