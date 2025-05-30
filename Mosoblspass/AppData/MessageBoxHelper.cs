using Mosoblspass.View.Dispatcher.Windows;
using System;
using System.Windows;

namespace Mosoblspass.AppData
{
    internal class MessageBoxHelper
    {
        public static void Error(Exception exception)
        {
            ShowCustom(exception.Message, exception.HelpLink ?? "Ошибка", MessageBoxButton.OK);
        }
        public static void Error(string text, string title = "Ошибка")
        {
            ShowCustom(text, title, MessageBoxButton.OK);
        }
        public static void Information(string text, string title = "Информация")
        {
            ShowCustom(text, title, MessageBoxButton.OK);
        }
        public static bool Question(string text, string title = "Вопрос", MessageBoxButton yesNo = default)
        {
            var result = ShowCustom(text, title, MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes;
        }
        public static void Warning(string text, string title = "Предупреждение")
        {
            ShowCustom(text, title, MessageBoxButton.OK);
        }
        private static MessageBoxResult ShowCustom(string text, string title, MessageBoxButton buttons)
        {
            var msgBox = new CustomMessageBoxWindow(text, title, buttons);
            msgBox.ShowDialog();
            return msgBox.Result;
        }
    }
}