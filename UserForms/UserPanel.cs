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
        public UserPanel(User user, string action, Dictionary<string, string> userData = null) : base(user)
        {
            this.user = user;
            Size = new Size(384, 421);

            Controls.Add(loginLabel);

            (userGroupBox.Controls["textBox"] as TextBox).SelectionStart = 0;
            Controls.Add(userGroupBox);

            Controls.Add(passwordGroupBox);

            Controls.Add(eMailGroupBox);

            Controls.Add(phoneGroupBox);

            if (user.Admin) Controls.Add(groupGroupBox);
            else
            {
                groupGroupBox = new UserDataGroupBox(
                Vars.EMail + ":", 41, 285,
                Properties.Resources.Group, Vars.UserEMailL);
                (groupGroupBox.Controls["textBox"] as TextBox).ReadOnly = true;
                Controls.Add(groupGroupBox);
            }

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
            {
                user.userForm.Text = Vars.UserData;
                loginLabel.Text += Vars.UserData;
                button.Text = Vars.Change;

                userGroupBox.Controls["textBox"].ForeColor = SystemColors.WindowText;
                userGroupBox.Controls["textBox"].Text = userData[Vars.UserName];

                userGroupBox.Controls["textBox2"].ForeColor = SystemColors.WindowText;
                userGroupBox.Controls["textBox2"].Text = userData[Vars.UserLastName];

                passwordGroupBox.Controls["textBox"].ForeColor = SystemColors.WindowText;
                passwordGroupBox.Controls["textBox"].Text = "";

                eMailGroupBox.Controls["textBox"].ForeColor = SystemColors.WindowText;
                eMailGroupBox.Controls["textBox"].Text = userData[Vars.UserEMail];

                phoneGroupBox.Controls["maskedTextBox"].ForeColor = SystemColors.WindowText;
                phoneGroupBox.Controls["maskedTextBox"].Text = userData[Vars.UserPhone];

                if (user.Admin)
                {
                    groupGroupBox.Controls["comboBox"].ForeColor = SystemColors.WindowText;
                    groupGroupBox.Controls["comboBox"].Text = userData[Vars.UserGroup];
                }
                else
                {
                    groupGroupBox.Controls["textBox"].ForeColor = SystemColors.WindowText;
                    groupGroupBox.Controls["textBox"].Text = userData[Vars.UserGroup];
                }


                (userGroupBox.Controls["textBox"] as TextBox).SelectionStart = 0;
            }
        }

        internal override bool UserTextBoxOK()
        {
            if (userGroupBox.TextBoxEmpty)
            {
                userGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Name + "!";
                return false;
            }

            if (userGroupBox.TextBox2Empty)
            {
                userGroupBox.Controls["textBox2"].ForeColor = Color.Red;
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

            if (button.Text == Vars.RegistrationButtonText ||
                button.Text == Vars.Add) { AddUser(); }
            else if (button.Text == Vars.Change) { ChangeUserData(); }
        }

        private void AddUser()
        {
            

            
        }
        private void ChangeUserData()
        {

        }
    }
}
