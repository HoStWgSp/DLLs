using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WorkWithUser.UserForms
{
    internal class RegistrationPanel : MainPanel
    {
        User user;

        Label loginLabel, errorLabel;

        GroupBox userGroupBox,
            passwordGroupBox,
            eMailGroupBox,
            phoneGroupBox;

        Button addButton;

        /// <summary>
        /// Панель для регистрации нового пользователя
        /// </summary>
        /// <param name="user"></param>
        public RegistrationPanel(User user):base(user)
        {
            this.user = user;
            UserAuthorized = false;

            loginLabel = new Label()
            {
                Text = Vars.Registration,
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(200, 30),
                Location = new Point(92, 5)
            };

            userGroupBox = new UserDataGroupBox(
                Vars.User + ":", 41, 45,
                Properties.Resources.User, Vars.UserNamesL, Vars.Name, false, false,
                true, Vars.LastName);
            userGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;

            passwordGroupBox = new UserDataGroupBox(
                Vars.Password + ":", 41, 105,
                Properties.Resources.Password, Vars.UserPasswordL, Vars.Password);
            passwordGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;

            eMailGroupBox = new UserDataGroupBox(
                Vars.EMail + ":", 41, 165,
                Properties.Resources.EMail, Vars.UserEMailL, Vars.EMail);
            eMailGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;

            phoneGroupBox = new UserDataGroupBox(
                Vars.Phone + ":", 41, 225,
                Properties.Resources.Phone, Vars.UserPhoneL, Vars.Phone, false, true);
            phoneGroupBox.Controls["maskedTextBox"].KeyDown += TextBox_KeyDown;

            errorLabel = new Label()
            {
                AutoSize = false,
                Name = "errorLabel",
                Size = new Size(303, 12),
                Font = new Font("Calibri", 8, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red
            };

            addButton = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 40),
                Text = Vars.Registration,
                TextAlign = ContentAlignment.MiddleCenter
            };
            addButton.Location = new Point(92, 285);
            addButton.Click += AddButton_Click;

            user.mainForm.Height = 374;
            user.mainForm.Location = new Point(
                (Screen.PrimaryScreen.Bounds.Width - user.mainForm.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - user.mainForm.Height) / 2);

            Controls.Add(loginLabel);
            Controls.Add(userGroupBox);
            Controls.Add(passwordGroupBox);
            Controls.Add(eMailGroupBox);
            Controls.Add(phoneGroupBox);
            Controls.Add(errorLabel);
            Controls.Add(addButton);

            Controls["errorLabel"].Visible = false;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (userGroupBox.Controls["textBox"].Text == Vars.Name)
            { LogInFormError(true); return; }

            if (userGroupBox.Controls["textBox2"].Text == Vars.LastName)
            {  LogInFormError(false, true); return; }

            if (passwordGroupBox.Controls["textBox"].Text == Vars.Password)
            { LogInFormError(false, false, true); return; }

            if (eMailGroupBox.Controls["textBox"].Text == Vars.EMail)
            { LogInFormError(false, false, false, true); return; }
            else
            {
                try
                {
                    var addr = new MailAddress(eMailGroupBox.Controls["textBox"].Text);
                }
                catch
                {
                    LogInFormError(false, false, false, false, true); return;
                }
            }

            if (phoneGroupBox.Controls["maskedTextBox"].Text == "+ (   )    -  -" ||
                !(phoneGroupBox.Controls["maskedTextBox"] as MaskedTextBox).MaskCompleted)
            { LogInFormError(false, false, false, false, false, true); return; }

            if (!UsersTableActions.Add(user.sqlConnection, userGroupBox.Controls["textBox"].Text,
                userGroupBox.Controls["textBox2"].Text,
                passwordGroupBox.Controls["textBox"].Text,
                eMailGroupBox.Controls["textBox"].Text,
                phoneGroupBox.Controls["maskedTextBox"].Text))
            {
                LogInFormError(false, false, false, false, false, false, true);
                return;
            }

            user.NewUserData.Add(Vars.UserName, userGroupBox.Controls["textBox"].Text);
            user.NewUserData.Add(Vars.UserLastName, userGroupBox.Controls["textBox2"].Text);
            user.NewUserData.Add(Vars.UserEMail, eMailGroupBox.Controls["textBox"].Text);
            user.NewUserData.Add(Vars.UserPhone, phoneGroupBox.Controls["maskedTextBox"].Text);

            UserAuthorized = true;

            user.mainForm.Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                addButton.PerformClick();
            }
        }

        /// <summary>
        /// Изменяет форму добавляя в нее сообщение об ошибке.
        /// </summary>
        /// <param name="loginError"></param>
        /// <param name="passwordError"></param>
        public void LogInFormError(bool nameEmpty, bool lastNameEmpty = false,
            bool passwordEmty = false, bool eMailEmpty = false, bool eMailIncorrect = false,
            bool phoneEmpty = false, bool userNotRegistred = false)
        {
            user.mainForm.Height = 384;
            errorLabel.Location = new Point(41, 275);
            addButton.Location = new Point(92, 295);

            Controls["errorLabel"].Visible = true;

            if (nameEmpty)
            {
                userGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Name + "!";
            }
            else if (lastNameEmpty)
            {
                userGroupBox.Controls["textBox2"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.LastName + "!";
            }
            else if (passwordEmty)
            {
                passwordGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Password + "!";
            }
            else if (eMailEmpty)
            {
                eMailGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Password + "!";
            }
            else if (eMailIncorrect)
            {
                eMailGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.EmailIncorrect + "!";
            }
            else if (phoneEmpty)
            {
                phoneGroupBox.Controls["maskedTextBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Phone + "!";
            }
            else if (userNotRegistred)
            {
                errorLabel.Text = Vars.UserNorRegistred;
            }
            user.mainForm.Refresh();
        }
    }
}
