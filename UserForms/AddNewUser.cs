using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWithUser.UserForms
{
    internal class AddNewUser : UserMainPanel
    {
        User user;

        public AddNewUser(User user ) : base( user)
        {

            this.user = user;
            Size = new Size(384, 421);

            Controls.Add(loginLabel);

            userGroupBox = new UserDataGroupBox(
                Vars.User + ":", 41, 45,
                Properties.Resources.User, Vars.UserTextL, Vars.Name, true, Vars.LastName);
            (userGroupBox.Controls["textBox"] as TextBox).SelectionStart = 0;
            Controls.Add(userGroupBox);
            userGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;
            userGroupBox.Controls["textBox2"].KeyDown += TextBox_KeyDown;

            дописать контролы

            groupGroupBox = new UserDataGroupBox(
                Vars.Group + ":", 41, 285,
                Properties.Resources.Group, user.groupList);
            groupGroupBox.Controls["comboBox"].KeyDown += TextBox_KeyDown;
            Controls.Add(groupGroupBox);

            user.userForm.Text = Vars.NewUserData;
            loginLabel.Text += Vars.NewUser;
            button.Text = Vars.Add;
            button.Click += Button_Click;
        }

        internal override void Button_Click(object sender, EventArgs e)
        {
            if (!UserTextBoxOK()) return;
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
                { Vars.UserPassword, passwordGroupBox.Controls["textBox"].Text}
            };

            if (user.UserData.Count == 0)
                newUser.Add(Vars.UserAdmin, "1");
            else
                newUser.Add(Vars.UserAdmin, "0");

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
