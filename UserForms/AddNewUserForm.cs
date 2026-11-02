using System;
using System.Drawing;
using UserData.UserForms.Elements;

namespace UserData.UserForms
{
    internal class AddNewUserForm : UserDataFormTemplate
    {
        private UserData userData;
        internal bool UserAdded { get; private set; } = false;

        public AddNewUserForm(UserData userData, string[] groups):
            base(userData.formIcon, groups, Vars.NewUser, Vars.Add)
        {
            this.userData = userData;
            ShowDialog();
        }

        internal override void Button_Click(object sender, EventArgs e)
        {
            if (!TextBoxesFillCorrect()) return;

            PasswordConfirmationForm passwordConfirmationForm = new PasswordConfirmationForm(passwordGroupBox.TextBoxText, userData.formIcon);
            
            if (!passwordConfirmationForm.PasswordConfirmed)
            {
                passwordGroupBox.TextBoxTextColor = Color.Red;
                errorLabel.Text = Vars.PasswordNotConfirmed + "!";
                return;
            }
            passwordGroupBox.TextBoxTextColor = SystemColors.WindowText;
            passwordGroupBox.TextBoxText = passwordConfirmationForm.Password;

            User newUser = new User()
            {
                UserPassword = PasswordHash.HashCode(passwordGroupBox.TextBoxText),
                UserName = nameGroupBox.TextBox1Text,
                UserMiddleName = nameGroupBox.TextBox2Text,
                UserLastName = lastNameGroupBox.TextBoxText,
                UserAddress = addressGroupBox.TextBoxText,
                UserEmail = eMailGroupBox.TextBoxText,
                UserGroup = groupsGroupBox.ComboBoxText,
                UserPhoneNumber = phoneNumberGroupBox.maskedTextBox.Text
            };

            if (!UsersTableActions.AddNewUser(userData, newUser)) return;

            UserAdded = true;

            newUser.UserId = userData.DB.FindRowIdInTable(
                $"SELECT Id " +
                $"FROM {userData.tableName} " +
                $"WHERE {Vars.UserName} = N'{newUser.UserName}' " +
                $"AND {Vars.UserMiddleName} = N'{newUser.UserMiddleName}' " +
                $"AND {Vars.UserLastName} = N'{newUser.UserLastName}' " +
                $"AND {Vars.UserEMail} = N'{newUser.UserEmail}' " +
                $"AND {Vars.UserPhone} = N'{newUser.UserPhoneNumber}'");
            
            userData.User = newUser;

            Close();
        }
    }
}
