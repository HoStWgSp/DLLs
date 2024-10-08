using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace WorkWithUser.UserForms
{
    internal class UserLogInPanel : UserMainPanel
    {
        User user;

        bool registration;

        /// <summary>
        /// Панель для проверки авторизации пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="registration"></param>
        public UserLogInPanel(User user, bool registration) : base(user)
        {
            this.user = user;
            this.registration = registration;

            Size = new Size(384, 241);

            loginLabel.Text = Vars.Authorization;
            Controls.Add(loginLabel);

            (userGroupBox.Controls["textBox"] as TextBox).SelectionStart = 0;
            Controls.Add(userGroupBox);

            Controls.Add(passwordGroupBox);

            errorLabel.Location = new Point(41, 155);
            Controls.Add(errorLabel);

            button.Location = new Point(92, 175);
            Controls.Add(button);

            if (!registration) return;
            
            registrationLabel.Location = new Point(92, 220);
            Controls.Add(registrationLabel);
        }

        internal override void RegistrationLabel_Click(object sender, EventArgs e)
        {
            UserActions.UserData(user, Vars.Registration);
            user.userForm.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - user.userForm.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - user.userForm.Height) / 2);
        }

        internal override void Button_Click(object sender, EventArgs e)
        {
            if (!UserTextBoxOK()) return;
            if (!PasswordTextBoxOK()) return;
            if (!CheckUserName(userGroupBox.Controls["textBox"].Text))
            {
                userGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.LoginNameError; 
                return;
            }

            DataRow userDataRow = Aauthorization(
                userGroupBox.Controls["textBox"].Text,
                passwordGroupBox.Controls["textBox"].Text);

            if (userDataRow == null)
            {
                passwordGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.PasswordError;
                return;
            }

            user.NewUserData.Add(Vars.UserName, userDataRow[Vars.UserName].ToString());
            user.NewUserData.Add(Vars.UserLastName, userDataRow[Vars.UserLastName].ToString());
            user.NewUserData.Add(Vars.UserEMail, userDataRow[Vars.UserEMail].ToString());
            user.NewUserData.Add(Vars.UserPhone, userDataRow[Vars.UserPhone].ToString());
            user.NewUserData.Add(Vars.UserGroup, userDataRow[Vars.UserGroup].ToString());

            user.userForm.Close();
        }

        /// <summary>
        /// Проверяет существует ли пользователь с таким именем.
        /// </summary>
        /// <param name="usersDataTable"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {
            foreach (DataRow dataRow in user.UsersData.Rows)
            {
                if ((dataRow[Vars.UserName].ToString() + " " + dataRow[Vars.UserLastName].ToString()) == userName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Проверяет существование пользователя в DataTable. И возвращает строку с данными найденного пользователя.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public DataRow Aauthorization(string userName, string password)
        {
            // Проверка существования пользователя            
            foreach (DataRow row in user.UsersData.Rows)
            {
                if ((row[Vars.UserName].ToString() + " " + row[Vars.UserLastName].ToString()) == userName && row[Vars.UserPassword].ToString() == password)
                    return row;
            }
            return null;
        }
    }
}
