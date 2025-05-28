using Mosoblspass.AppData;
using Mosoblspass.Model;
using Mosoblspass.View.Dispatcher.Pages;
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
        public DispatcherMainWindow()
        {
            InitializeComponent();
            SearchPage searchPage = new SearchPage();
            MainFrame.Navigate(searchPage);
            FrameHelper.selectedFrame = MainFrame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profilePage = new ProfilePage();
            MainFrame.Navigate(profilePage);
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchPage searchPage = new SearchPage();
            MainFrame.Navigate(searchPage);

        }

        private void DocumsBtn_Click(object sender, RoutedEventArgs e)
        {
            DocumentsPage documentPage = new DocumentsPage();
            MainFrame.Navigate(documentPage);
        }

        private void JournalBtn_Click(object sender, RoutedEventArgs e)
        {
            JournalPage journalPage = new JournalPage();
            MainFrame.Navigate(journalPage);
        }
    }
}
