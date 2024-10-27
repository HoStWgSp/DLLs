using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;

namespace UserData.UserForms
{
    internal class UserDataPanel : UserMainPanel
    {
        UserData user;
        Dictionary<string, string> userData;

        /// <summary>
        /// Панель для регистрации нового пользователя
        /// </summary>
        /// <param name="user"></param>
        public UserDataPanel(UserData user, Dictionary<string, string> userData = null):
            base(user, Vars.UserData, Vars.Administrator, Vars.Change)
        {
            this.user = user;
            this.userData = userData;

            if (userData.Count > 0)
            {
                userGroupBox.Controls["textBox"].ForeColor = SystemColors.WindowText;
                userGroupBox.Controls["textBox"].Text = userData[Vars.UserName].ToString();

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
                    if (userData[Vars.UserAdmin].ToString() == "1")
                        adminCheckBox.Checked = true;
                }
                else
                {
                    groupGroupBox.Controls["textBox"].ForeColor = SystemColors.WindowText;
                    groupGroupBox.Controls["textBox"].Text = userData[Vars.UserGroup];
                }
            }
        }

        

        internal override void Button_Click(object sender, EventArgs e)
        {
            if (!UserTextBoxOK(true)) return;
            if (!EMailTextBoxOK()) return;
            if (!PhoneMaskedTextBoxOK()) return;

            if (user.Admin)
                if (!GroupComboBoxOK()) return;

            if (passwordGroupBox.Controls["textBox"].Text != "")
            {
                //AlertConfirm.AlertConfirm alertConfirm = new AlertConfirm.AlertConfirm(user.formIcon);
                //if (!alertConfirm.Confirmation(Vars.UserPasswordChange, 300, 100)) return;
            }

            string ugroup;
            string admin;
            if (user.Admin)
            {
                ugroup = groupGroupBox.Controls["comboBox"].Text;
                if (adminCheckBox.Checked)
                    admin = "1";
                else 
                    admin = "0";
            }
            else
            {
                ugroup = groupGroupBox.Controls["textBox"].Text;
                admin = userData[Vars.UserAdmin];
            }

            bool changed = false;

            if (userData[Vars.UserName] != userGroupBox.Controls["textBox"].Text ||
                userData[Vars.UserLastName] != userGroupBox.Controls["textBox2"].Text ||
                userData[Vars.UserEMail] != eMailGroupBox.Controls["textBox"].Text ||
                userData[Vars.UserPhone] != phoneGroupBox.Controls["maskedTextBox"].Text ||
                userData[Vars.UserGroup] != ugroup ||
                admin != userData[Vars.UserAdmin])
            {
                Dictionary<string, string> userNewData = new Dictionary<string, string>()
                {
                    { Vars.UserName, userGroupBox.Controls["textBox"].Text },
                    { Vars.UserLastName, userGroupBox.Controls["textBox2"].Text },
                    { Vars.UserEMail, eMailGroupBox.Controls["textBox"].Text },
                    { Vars.UserPhone, phoneGroupBox.Controls["maskedTextBox"].Text },
                    { Vars.UserGroup, ugroup },
                    { Vars.UserAdmin, admin }
                };

                if (!UsersTableActions.Change(user, Convert.ToInt32(userData["Id"]), userNewData))
                    return;
                changed = true;
            }
            if (passwordGroupBox.Controls["textBox"].Text != "")
            {
                if (!UsersTableActions.PasswordChange(user, Convert.ToInt32(userData["Id"]), passwordGroupBox.Controls["textBox"].Text))
                    return;
                changed = true;
            }

            if (changed)
                user.userForm.Close();
        }
    }
}
