using Mosoblspass.AppData;
using Mosoblspass.View.Dispatcher.Pages;
using System.Windows;

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