using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using WorkWithUser.UserForms;
using System.Drawing;

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
        public static bool Authorization(User user, Dictionary<string, Image> images, bool registration = false)
        {
            AlertConfirm.AlertConfirm alertConfirm = new AlertConfirm.AlertConfirm();

            user.mainForm.Text = Vars.Authorization + " " + Vars.User2;
            user.mainForm.Controls.Clear();
            user.mainForm.mainPanel = new LogInPanel(user, images, registration);
            user.mainForm.Controls.Add(user.mainForm.mainPanel);
            user.mainForm.ShowDialog();

            return user.mainForm.mainPanel.UserAuthorized;
        }


        public static bool Registration()
        {
            return true;
        }
    }
}
