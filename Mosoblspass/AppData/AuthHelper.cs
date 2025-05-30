using Mosoblspass.Model;
using System.Linq;

namespace Mosoblspass.AppData
{
    public static class AuthHelper
    {
        public static User selectedUser;
        private static MosoblpoghspasEntities _context = App.GetContext();
        public static bool Authenticate(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBoxHelper.Error("Не все поля для ввода были заполнены.");
                return false;
            }
            selectedUser = _context.Users.FirstOrDefault(user => user.Login == login && user.Password == password);
            if (selectedUser != null)
            {
                App.CurrentUser = selectedUser;
                return true;
            }
            else
            {
                MessageBoxHelper.Error("Неправильно введен логин или пароль.");
                return false;
            }
        }
    }
}