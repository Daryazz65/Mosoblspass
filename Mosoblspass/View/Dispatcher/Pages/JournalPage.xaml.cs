using Mosoblspass.AppData;
using System.Windows.Controls;

namespace Mosoblspass.View.Dispatcher.Pages
{
    public partial class JournalPage : Page
    {
        public JournalPage()
        {
            InitializeComponent();
            HistoryListBox.ItemsSource = SearchHistory.History;
        }
    }
}