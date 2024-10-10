using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;

namespace WorkWithUser.UserForms
{
    internal class UserChangePanel : UserMainPanel
    {
        User user;
        Dictionary<string, string> userData;

        /// <summary>
        /// Панель для регистрации нового пользователя
        /// </summary>
        /// <param name="user"></param>
        public UserChangePanel(User user, Dictionary<string, string> userData = null) : base(user)
        {
            this.user = user;
            this.userData = userData;
            Size = new Size(384, 421);

            LoginLabel(Vars.UserData);
            UserGroupBox(true);
            PasswordGroupBox();
            EMailGroupBox();
            PhoneGroupBox();
            ErrorLabel();
            ButtonBody(Vars.Change);

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
                GroupGroupBox();
                groupGroupBox.Controls["comboBox"].ForeColor = SystemColors.WindowText;
                groupGroupBox.Controls["comboBox"].Text = userData[Vars.UserGroup];
            }
            else
            {
                GroupGroupBox(false);
                groupGroupBox.Controls["textBox"].ForeColor = SystemColors.WindowText;
                groupGroupBox.Controls["textBox"].Text = userData[Vars.UserGroup];
            }
        }

        

        internal override void Button_Click(object sender, EventArgs e)
        {
            продолжить писать изменение 
            if (!UserTextBoxOK()) return;
            if (!EMailTextBoxOK()) return;
            if (!PhoneMaskedTextBoxOK()) return;

            Dictionary<string, string> userNewData = new Dictionary<string, string>()
            {
                { Vars.UserName, userGroupBox.Controls["textBox"].Text },
                { Vars.UserLastName, userGroupBox.Controls["textBox2"].Text },
                { Vars.UserPassword, passwordGroupBox.Controls["textBox"].Text },
                { Vars.UserEMail, eMailGroupBox.Controls["textBox"].Text },
                { Vars.UserPhone, phoneGroupBox.Controls[Vars.UserPhone].Text }
            };

            if (user.Admin)
                userNewData.Add(Vars.UserGroup, groupGroupBox.Controls["comboBox"].Text);
            else
                userNewData.Add(Vars.UserGroup, groupGroupBox.Controls["textBox"].Text);




            if (!GroupComboBoxOK()) return;

            if (PasswordTextBoxOK())
            {
                AlertConfirm.AlertConfirm alertConfirm = new AlertConfirm.AlertConfirm(user.formIcon);
                if (!alertConfirm.Confirmation(Vars.UserPasswordChange, 200, 100)) return;
            }

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
