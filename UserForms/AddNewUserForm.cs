using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserData.UserForms.Elements;

namespace UserData.UserForms
{
    internal class AddNewUserForm : UserDataFormTemplate
    {
        private UserData userData;

        public AddNewUserForm(UserData userData, string[] groups, bool addAdmin = false):
            base(userData.formIcon, groups, Vars.NewUser, Vars.Add, addAdmin)
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

            if (!UsersTableActions.AddNewUser(userData.dataBase, newUser)) return;

            newUser.Id = userData.dataBase.FindRowIdInTable(
                $"SELECT Id " +
                $"FROM {Vars.TableName} " +
                $"WHERE {Vars.UserName} = '{newUser.UserName}' " +
                $"AND {Vars.UserMiddleName} = '{newUser.UserMiddleName}' " +
                $"AND {Vars.UserLastName} = '{newUser.UserLastName}' " +
                $"AND {Vars.UserEMail} = '{newUser.UserEmail}' " +
                $"AND {Vars.UserPhone} = '{newUser.UserPhoneNumber}'");
            
            userData.User = newUser;

            Close();
        }
    }
}
