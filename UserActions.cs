using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UserData.MyElements;
using UserData.UserForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UserData
{
    internal class UserActions
    {
        internal UserData UD { get; set; }

        public UserActions (UserData userData)
        {
            UD = userData;
        }


        /// <summary>
        /// Добавляет нового пользователя и возвращает его данные.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="images"></param>
        internal void UserData(UserData user, string action, Dictionary<string, string> userData = null)
        {
            //user.userForm.Controls.Clear();
            //if (userData == null)
            //    user.userForm.mainPanel = new AddNewUser(user);
            //else
            //    user.userForm.mainPanel = new UserDataPanel(user, userData);
            //user.userForm.Controls.Add(user.userForm.mainPanel);
            //user.userForm.ClientSize = new Size(user.userForm.mainPanel.Width, user.userForm.mainPanel.Height);
            //user.userForm.Refresh();
            //(user.userForm.mainPanel.Controls["userGroupBox"].Controls["textBox"] as TextBox).SelectionStart = 0;
        }

        /// <summary>
        /// Позволяет работать со списком пользователей
        /// </summary>
        /// <param name="user"></param>
        internal void UserList(UserData user)
        {
            user.userListForm.Text = Vars.UserList;
            user.userListForm.usersList = new UsersList(user);
            user.userListForm.Controls.Add(user.userListForm.usersList);
            //user.userListForm.Refresh();
        }
    }
}
