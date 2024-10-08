using System.Drawing;
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
            user.userForm.Text = Vars.Authorization + " " + Vars.User2;
            user.userForm.Controls.Clear();
            user.userForm.mainPanel = new UserLogInPanel(user, registration);
            user.userForm.ClientSize = new Size(user.userForm.mainPanel.Width, user.userForm.mainPanel.Height);
            user.userForm.Controls.Add(user.userForm.mainPanel);
            user.userForm.Refresh();
            (user.userForm.mainPanel.Controls["userGroupBox"].Controls["textBox"] as TextBox).Focus();
        }

        /// <summary>
        /// Добавляет нового пользователя и возвращает его данные.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="images"></param>
        public static void UserData(User user, string action)
        {
            user.userForm.Controls.Clear();
            user.userForm.mainPanel = new UserPanel(user, action);
            user.userForm.Controls.Add(user.userForm.mainPanel);
            user.userForm.ClientSize = new Size(user.userForm.mainPanel.Width, user.userForm.mainPanel.Height);
            user.userForm.Refresh();
            user.userForm.mainPanel.Controls["userGroupBox2"].Controls["textBox"].Focus();
        }

        /// <summary>
        /// Позволяет работать со списком пользователей
        /// </summary>
        /// <param name="user"></param>
        public static void UserList(User user)
        {
            user.userListForm.Text = Vars.UserList;
            user.userListForm.usersList = new UsersList(user);
            user.userListForm.Controls.Add(user.userListForm.usersList);
            //user.userListForm.Refresh();
        }
    }
}
