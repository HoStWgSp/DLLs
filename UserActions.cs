using System.Windows.Forms;
using WorkWithUser.UserForms;

namespace WorkWithUser
{
    internal class UserActions
    {
        /// <summary>
        /// Проверяет существование пользователя и возвращает его данные.
        /// Иконка на форму обязательно (формат ico).
        /// Картинки на пользователя желательно, но не обязательно. (Формат png).
        /// Registration - true если вы хотите, что бы на форме была ссылка на регистрацию.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void Authorization(User user, bool registration = false)
        {
            user.mainForm.Text = Vars.Authorization + " " + Vars.User2;
            user.mainForm.Controls.Clear();
            user.mainForm.mainPanel = new LogInPanel(user, registration);
            user.mainForm.Controls.Add(user.mainForm.mainPanel);
            user.mainForm.Refresh();
            (user.mainForm.mainPanel.Controls["userGroupBox"].Controls["textBox"] as TextBox).Focus();
        }

        /// <summary>
        /// Добавляет нового пользователя и возвращает его данные.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="images"></param>
        public static void AddNewUser(User user)
        {
            user.mainForm.Text = Vars.RegistrationText;
            user.mainForm.Controls.Clear();
            user.mainForm.mainPanel = new AddUserPanel(user);
            user.mainForm.Controls.Add(user.mainForm.mainPanel);
            user.mainForm.Refresh();
            (user.mainForm.mainPanel.Controls["userGroupBox"].Controls["textBox"] as TextBox).Focus();
        }

        /// <summary>
        /// Позволяет работать со списком пользователей
        /// </summary>
        /// <param name="user"></param>
        public static void UserList(User user)
        {
            user.mainForm.Text = Vars.UserList;
            user.mainForm.Controls.Clear();
            user.mainForm.mainPanel = new UserListPanel(user);
            user.mainForm.Controls.Add(user.mainForm.mainPanel);
            user.mainForm.Refresh();
        }
    }
}
