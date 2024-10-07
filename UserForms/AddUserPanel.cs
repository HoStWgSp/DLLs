using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;

namespace WorkWithUser.UserForms
{
    internal class AddUserPanel : MainPanel
    {
        User user;

        Label loginLabel, errorLabel;

        GroupBox userGroupBox,
            passwordGroupBox,
            eMailGroupBox,
            phoneGroupBox,
            groupGroupBox;

        Button addButton;

        /// <summary>
        /// Панель для регистрации нового пользователя
        /// </summary>
        /// <param name="user"></param>
        public AddUserPanel(User user) : base(user)
        {
            this.user = user;

            loginLabel = new Label()
            {
                Text = Vars.Registration,
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(200, 30),
                Location = new Point(92, 5)
            };
            Controls.Add(loginLabel);

            userGroupBox = new UserDataGroupBox(
                Vars.User + ":", 41, 45,
                Properties.Resources.User, Vars.UserTextL,
                false, Vars.Name, true, Vars.LastName);
            userGroupBox.Name = "userGroupBox";
            userGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;
            (userGroupBox.Controls["textBox"] as TextBox).SelectionStart = 0;
            Controls.Add(userGroupBox);

            passwordGroupBox = new UserDataGroupBox(
                Vars.Password + ":", 41, 105,
                Properties.Resources.Password, Vars.UserTextL, true, Vars.Password);
            passwordGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;
            Controls.Add(passwordGroupBox);

            eMailGroupBox = new UserDataGroupBox(
                Vars.EMail + ":", 41, 165, 
                Properties.Resources.EMail, Vars.UserEMailL, false, Vars.EMail);
            eMailGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;
            Controls.Add(eMailGroupBox);

            phoneGroupBox = new UserDataGroupBox(
                Vars.Phone + ":", 41, 225,
                Properties.Resources.Phone, "+0(000) 000-00-00");
            phoneGroupBox.Controls["maskedTextBox"].KeyDown += TextBox_KeyDown;
            Controls.Add(phoneGroupBox);

            groupGroupBox = new UserDataGroupBox(
                Vars.Group + ":", 41, 285,
                Properties.Resources.Group, user.groupList);
            groupGroupBox.Controls["comboBox"].KeyDown += TextBox_KeyDown;
            Controls.Add(groupGroupBox);

            errorLabel = new Label()
            {
                AutoSize = false,
                Name = "errorLabel",
                Size = new Size(303, 12),
                Font = new Font("Calibri", 8, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red
            };
            Controls.Add(errorLabel);
            Controls["errorLabel"].Visible = false;

            addButton = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 40),
                Text = Vars.Registration,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(92, 345)
            };
            addButton.Click += AddButton_Click;
            Controls.Add(addButton);

            user.mainForm.Height = 434;
            user.mainForm.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - user.mainForm.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - user.mainForm.Height) / 5);
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

            if (groupGroupBox.Controls["comboBox"].Text == "" ||
                groupGroupBox.Controls["comboBox"].Text == null)
            { LogInFormError(false, false, false, false, false, false, true); return; }

            Dictionary<string, string> newUser = new Dictionary<string, string>()
            {
                { Vars.UserName, userGroupBox.Controls["textBox"].Text },
                { Vars.UserLastName, userGroupBox.Controls["textBox2"].Text },
                { Vars.UserEMail, eMailGroupBox.Controls["textBox"].Text },
                { Vars.UserPhone, phoneGroupBox.Controls["maskedTextBox"].Text },
                { Vars.UserGroup, groupGroupBox.Controls["comboBox"].Text} ,
                { Vars.UserPassword, passwordGroupBox.Controls["textBox"].Text}
            };

            if (!UsersTableActions.Add(user.sqlConnection, newUser))
            {
                LogInFormError(false, false, false, false, false, false, false, true);
                return;
            }

            newUser.Remove(Vars.UserPassword);
            user.NewUserData = new Dictionary<string, string>(newUser);
            
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
            bool phoneEmpty = false, bool groupNotChoosen = false, bool userNotRegistred = false)
        {
            user.mainForm.Height = 444;
            errorLabel.Location = new Point(41, 335);
            addButton.Location = new Point(92, 355);

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
            else if (groupNotChoosen)
            {
                errorLabel.Text = Vars.GroupNotChoosen;
            }
            user.mainForm.Refresh();
        }
    }
}
