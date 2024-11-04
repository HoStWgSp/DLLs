using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements;

namespace UserData.UserForms
{
    internal class AddNewUserForm : UserDataFormTemplate
    {
        private UserData UserData { get; set; }
        public bool UserAdded { get; private set; } = false;
        private string password;

        public AddNewUserForm(UserData userData, bool addAdmin = false):
            base(userData.formIcon, userData.groupsList, Vars.NewUser, Vars.Add, addAdmin)
        {
            UserData = userData;
            ShowDialog();
        }

        internal override void Button_Click(object sender, EventArgs e)
        {
            if (TextBoxesEmptyOrWaterMarked()) return;
            if (passwordGroupBox.textBox.Text != Vars.Password &&
                passwordGroupBox.textBox.Text != "")
            {
                if (password != passwordGroupBox.textBox.Text ||
                    passwordGroupBox.textBox.ForeColor == Color.Red)
                {
                    PasswordConfirmationForm passwordConfirmationForm = new PasswordConfirmationForm(passwordGroupBox.textBox.Text, UserData.formIcon);
                    if (!passwordConfirmationForm.PasswordConfirmed)
                    {
                        passwordGroupBox.textBox.ForeColor = Color.Red;
                        errorLabel.Text = Vars.PasswordNotConfirmed + "!";
                        return;
                    }
                    passwordGroupBox.textBox.ForeColor = SystemColors.WindowText;
                    passwordGroupBox.textBox.Text = passwordConfirmationForm.Password;
                    password = passwordGroupBox.textBox.Text;
                }
            }

            User newUser = new User()
            {
                UserPassword = PasswordHash.HashCode(passwordGroupBox.textBox.Text),
                UserName = nameGroupBox.textBox1.Text,
                UserMiddleName = nameGroupBox.textBox2.Text,
                UserLastName = lastNameGroupBox.textBox.Text,
                UserAddress = addressGroupBox.textBox.Text,
                UserEmail = eMailGroupBox.textBox.Text,
                UserGroup = groupsGroupBox.comboBox.Text,
                UserPhoneNumber = phoneNumberGroupBox.maskedTextBox.Text
            };

            if (adminCheckBox.Checked) newUser.UserAdmin = "1";
            else newUser.UserAdmin = "0";

            if (!UserData.UsersTableActions.AddNewUser(newUser)) return;

            UserAdded = true;

            Close();
        }
    }
}
