﻿using System.Windows;

namespace Mosoblspass.View.Dispatcher.Windows
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBoxWindow.xaml
    /// </summary>
    public partial class CustomMessageBoxWindow : Window
    {
        public MessageBoxResult Result { get; private set; } = MessageBoxResult.None;
        public CustomMessageBoxWindow(string message, string title, MessageBoxButton buttons)
        {
            InitializeComponent();
            TitleBlock.Text = title;
            MessageBlock.Text = message;
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    OkBtn.Visibility = Visibility.Visible;
                    YesBtn.Visibility = Visibility.Collapsed;
                    NoBtn.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNo:
                    OkBtn.Visibility = Visibility.Collapsed;
                    YesBtn.Visibility = Visibility.Visible;
                    NoBtn.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }
        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Close();
        }
        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Close();
        }
    }
}