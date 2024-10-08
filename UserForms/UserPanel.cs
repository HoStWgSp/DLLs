using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;

namespace WorkWithUser.UserForms
{
    internal class UserPanel : UserMainPanel
    {
        User user;

        /// <summary>
        /// Панель для регистрации нового пользователя
        /// </summary>
        /// <param name="user"></param>
        public UserPanel(User user, string action) : base(user)
        {
            this.user = user;
            Size = new Size(384, 421);

            Controls.Add(loginLabel);

            (userGroupBox2.Controls["textBox"] as TextBox).SelectionStart = 0;
            Controls.Add(userGroupBox2);

            Controls.Add(passwordGroupBox);

            Controls.Add(eMailGroupBox);

            Controls.Add(phoneGroupBox);

            Controls.Add(groupGroupBox);

            Controls.Add(errorLabel);

            Controls.Add(button);

            if (action == Vars.Registration)
            {
                user.userForm.Text = Vars.RegistrationText;
                loginLabel.Text = Vars.Registration;
                button.Text = Vars.RegistrationButtonText;
                button.Click += Button_Click;
            }
            if (action == Vars.Add)
            {
                user.userForm.Text = Vars.NewUserData;
                loginLabel.Text += Vars.NewUser;
                button.Text = Vars.Add;
                button.Click += Button_Click;
            }
            if (action == Vars.Change)
                user.userForm.Text = Vars.UserData;
        }

        internal override bool UserTextBoxOK()
        {
            if (userGroupBox2.TextBoxEmpty)
            {
                userGroupBox2.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Name + "!";
                return false;
            }

            if (userGroupBox2.TextBox2Empty)
            {
                userGroupBox2.Controls["textBox2"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.LastName + "!";
                return false;
            }

            return true;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (!UserTextBoxOK()) return; 
            if (!PasswordTextBoxOK()) return;
            if (!EMailTextBoxOK()) return;
            if (!PhoneMaskedTextBoxOK()) return;
            if (!GroupComboBoxOK()) return;

            Dictionary<string, string> newUser = new Dictionary<string, string>()
            {
                { Vars.UserName, userGroupBox2.Controls["textBox"].Text },
                { Vars.UserLastName, userGroupBox2.Controls["textBox2"].Text },
                { Vars.UserEMail, eMailGroupBox.Controls["textBox"].Text },
                { Vars.UserPhone, phoneGroupBox.Controls["maskedTextBox"].Text },
                { Vars.UserGroup, groupGroupBox.Controls["comboBox"].Text} ,
                { Vars.UserPassword, passwordGroupBox.Controls["textBox"].Text}
            };

            if (button.Text == Vars.RegistrationButtonText ||
                button.Text == Vars.Add) { AddUser(newUser); }
        }

        private void AddUser(Dictionary<string, string> newUser)
        {
            if (!UsersTableActions.Add(user.sqlConnection, newUser))
            {
                errorLabel.Text = Vars.UserNorRegistred;
                return;
            }

            newUser.Remove(Vars.UserPassword);
            user.NewUserData = new Dictionary<string, string>(newUser);

            user.userForm.Close();
        }
    }
}
