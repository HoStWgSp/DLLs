using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserData.UserForms
{
    internal class AddNewUser : UserMainPanel
    {
        User user;

        public AddNewUser(User user):
            base(user, Vars.NewUser, Vars.Administrator, Vars.Add)
        {
            this.user = user;
            Size = new Size(384, 421);

            user.userForm.Text = Vars.NewUserData;
        }

        internal override void Button_Click(object sender, EventArgs e)
        {
            if (!UserTextBoxOK(true)) return;
            if (!PasswordTextBoxOK()) return;
            if (!EMailTextBoxOK()) return;
            if (!PhoneMaskedTextBoxOK()) return;
            if (!GroupComboBoxOK()) return;

            Dictionary<string, string> newUser = new Dictionary<string, string>()
            {
                { Vars.UserName, userGroupBox.Controls["textBox"].Text },
                { Vars.UserLastName, userGroupBox.Controls["textBox2"].Text },
                { Vars.UserEMail, eMailGroupBox.Controls["textBox"].Text },
                { Vars.UserPhone, phoneGroupBox.Controls["maskedTextBox"].Text },
                { Vars.UserGroup, groupGroupBox.Controls["comboBox"].Text} ,
                { Vars.UserPassword, passwordGroupBox.Controls["textBox"].Text},
                { Vars.UserAdmin, "0"}
            };

            if (user.Admin)
            {
                if (adminCheckBox.Checked)
                    newUser.Add(Vars.UserAdmin, "1");
                else
                    newUser.Add(Vars.UserAdmin, "");
            }

            if (!UsersTableActions.Add(user, newUser))
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
