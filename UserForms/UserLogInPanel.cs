using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace WorkWithUser.UserForms
{
    internal class UserLogInPanel : UserMainPanel
    {
        //User user;

        bool registration;

        /// <summary>
        /// Панель для проверки авторизации пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="registration"></param>
        public UserLogInPanel(User user, bool registration) : base(user)
        {
            //this.user = user;
            this.registration = registration;

            Size = new Size(384, 241);

            loginLabel.Text = Vars.Authorization;
            Controls.Add(loginLabel);

            userGroupBox = new UserDataGroupBox(
                Vars.User + ":", 41, 45,
                Properties.Resources.User, Vars.UserTextL, Vars.NameAndLastName);
            userGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;
            (userGroupBox.Controls["textBox"] as TextBox).SelectionStart = 0;
            Controls.Add(userGroupBox);

            (passwordGroupBox.Controls["textBox"] as TextBox).PasswordChar = '*';
            Controls.Add(passwordGroupBox);

            errorLabel.Location = new Point(41, 155);
            Controls.Add(errorLabel);

            button.Location = new Point(92, 175);
            Controls.Add(button);

            if (!registration) return;
            registrationLabel = new Label()
            {
                AutoSize = false,
                Size = new Size(200, 16),
                Text = Vars.GetRegistration,
                Font = new Font("Calibri", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.Blue
            };
            registrationLabel.Click += RegistrationLabel_Click;
            registrationLabel.MouseEnter += RegistrationLabel_MouseEnter;
            registrationLabel.MouseLeave += RegistrationLabel_MouseLeave;
            registrationLabel.Location = new Point(92, 220);
            Controls.Add(registrationLabel);
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
            user.NewUserData.Add(Vars.UserAdmin, userDataRow[Vars.UserAdmin].ToString());

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
