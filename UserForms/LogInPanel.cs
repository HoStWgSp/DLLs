using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace WorkWithUser.UserForms
{
    internal class LogInPanel : MainPanel
    {
        User user;

        Label loginLabel,
            errorLabel,
            registrationLabel;

        GroupBox userGroupBox,
            passwordGroupBox;

        Button logInButton;

        bool registration;

        /// <summary>
        /// Панель для проверки авторизации пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="registration"></param>
        public LogInPanel(User user, bool registration) : base(user)
        {
            this.user = user;
            this.registration = registration;

            loginLabel = new Label()
            {
                Text = Vars.Authorization,
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(200, 30),
                Location = new Point(92, 5)
            };

            userGroupBox = new UserDataGroupBox(
                Vars.User + ":", 41, 45,
                Properties.Resources.User, Vars.UserTextL, false, Vars.NameAndLastName);
            userGroupBox.Name = "userGroupBox";
            userGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;
            (userGroupBox.Controls["textBox"] as TextBox).SelectionStart = 0;

            passwordGroupBox = new UserDataGroupBox(
                Vars.Password + ":", 41, 105,
                Properties.Resources.Password, Vars.UserTextL, false, Vars.Password);
            passwordGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;

            errorLabel = new Label()
            {
                AutoSize = false,
                Name = "errorLabel",
                Size = new Size(303, 12),
                Font = new Font("Calibri", 8, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red
            };

            logInButton = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 40),
                Text = Vars.Enter,
                TextAlign = ContentAlignment.MiddleCenter
            };
            logInButton.Location = new Point(92, 165);
            logInButton.Click += LogInButton_Click;

            if (registration)
            {
                registrationLabel = new Label()
                {
                    AutoSize = false,
                    Size = new Size(200, 16),
                    Text = Vars.GetRegistration,
                    Font = new Font("Calibri", 8, FontStyle.Bold),
                    TextAlign = ContentAlignment.TopCenter,
                    ForeColor = Color.Blue
                };
                user.mainForm.Size = new Size(400, 270);
                registrationLabel.Location = new Point(92, 210);
                registrationLabel.Click += RegistrationLabel_Click;
                registrationLabel.MouseEnter += RegistrationLabel_MouseEnter;
                registrationLabel.MouseLeave += RegistrationLabel_MouseLeave;
                Controls.Add(registrationLabel);
            }

            Controls.Add(loginLabel);
            Controls.Add(userGroupBox);
            Controls.Add(passwordGroupBox);
            Controls.Add(errorLabel);
            Controls.Add(logInButton);

            Controls["errorLabel"].Visible = false;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                logInButton.PerformClick();
            }
        }

        private void RegistrationLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void RegistrationLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private void RegistrationLabel_Click(object sender, EventArgs e)
        {
            UserActions.AddNewUser(user);
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            if (userGroupBox.Controls["textBox"].Text == Vars.NameAndLastName)
            { LogInFormError(true); return; }
            else if (passwordGroupBox.Controls["textBox"].Text == Vars.Password)
            { LogInFormError(false, true); return; }
            else if (!CheckUserName(userGroupBox.Controls["textBox"].Text))
            { LogInFormError(false, false, true); return; }

            DataRow userDataRow = Aauthorization(
                userGroupBox.Controls["textBox"].Text,
                passwordGroupBox.Controls["textBox"].Text);

            if (userDataRow == null)
            { LogInFormError(false, false, false, true); return; }

            user.NewUserData.Add(Vars.UserName, userDataRow[Vars.UserName].ToString());
            user.NewUserData.Add(Vars.UserLastName, userDataRow[Vars.UserLastName].ToString());
            user.NewUserData.Add(Vars.UserEMail, userDataRow[Vars.UserEMail].ToString());
            user.NewUserData.Add(Vars.UserPhone, userDataRow[Vars.UserPhone].ToString());
            user.NewUserData.Add(Vars.UserGroup, userDataRow[Vars.UserGroup].ToString());

            user.mainForm.Close();
        }

        /// <summary>
        /// Изменяет форму добавляя в нее сообщение об ошибке.
        /// </summary>
        /// <param name="loginError"></param>
        /// <param name="passwordError"></param>
        public void LogInFormError( bool loginEmpty, bool passwordEmty = false, bool loginError = false, bool passwordError = false)
        {
            user.mainForm.Height = 264;
            errorLabel.Location = new Point(41, 155);
            logInButton.Location = new Point(92, 175);

            if (registration)
            {
                registrationLabel.Location = new Point(92, 220);
                user.mainForm.Size = new Size(400, 280);
            }

            Controls["errorLabel"].Visible = true;

            if (loginEmpty)
            {
                userGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.User + "!";
            }
            else if (passwordEmty)
            { 
                passwordGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Password + "!";

            }
            else if (loginError)
            {
                userGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.LoginNameError;
            }
            else if (!loginError && passwordError)
            {
                passwordGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.PasswordError;
            }
            user.mainForm.Refresh();
        }

        /// <summary>
        /// Проверяет существует ли пользователь с таким именем.
        /// </summary>
        /// <param name="usersDataTable"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {
            foreach (DataRow dataRow in user.usersData.Rows)
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
            foreach (DataRow row in user.usersData.Rows)
            {
                if ((row[Vars.UserName].ToString() + " " + row[Vars.UserLastName].ToString()) == userName && row[Vars.UserPassword].ToString() == password)
                    return row;
            }
            return null;
        }
    }
}
