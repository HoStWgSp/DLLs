using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserData.MyElements;
using UserData.UserForms.Elements;

namespace UserData.UserForms
{
    internal class UserDataForm : UserDataFormTemplate
    {
        private UserData UserData { get; set; }
        User user;
        public bool UserDataChanged { get; private set; } = false;
        private string password;
        private bool change = false, checkBoxState = false;

        public UserDataForm(UserData userData, User user, bool admin = false):
            base(userData.formIcon, userData.groupsList, Vars.UserData, Vars.Change, admin)
        {
            UserData = userData;
            this.user = user;

            nameGroupBox.textBox1.ForeColor = SystemColors.WindowText;
            nameGroupBox.textBox1.Text = user.UserName;
            nameGroupBox.textBox1.TextChanged += TextBox_TextChanged;

            nameGroupBox.textBox2.ForeColor = SystemColors.WindowText;
            nameGroupBox.textBox2.Text = user.UserMiddleName;
            nameGroupBox.textBox2.TextChanged += TextBox_TextChanged;

            lastNameGroupBox.textBox.ForeColor = SystemColors.WindowText;
            lastNameGroupBox.textBox.Text = user.UserLastName;
            lastNameGroupBox.textBox.TextChanged += TextBox_TextChanged;

            passwordGroupBox.textBox.TextChanged += TextBox_TextChanged;

            phoneNumberGroupBox.maskedTextBox.ForeColor = SystemColors.WindowText;
            phoneNumberGroupBox.maskedTextBox.Text = user.UserPhoneNumber;
            phoneNumberGroupBox.maskedTextBox.TextChanged += TextBox_TextChanged;

            eMailGroupBox.textBox.ForeColor = SystemColors.WindowText;
            eMailGroupBox.textBox.Text = user.UserEmail;
            eMailGroupBox.textBox.TextChanged += TextBox_TextChanged;

            addressGroupBox.textBox.ForeColor = SystemColors.WindowText;
            addressGroupBox.textBox.Text = user.UserAddress;
            addressGroupBox.textBox.TextChanged += TextBox_TextChanged;

            groupsGroupBox.comboBox.ForeColor = SystemColors.WindowText;
            groupsGroupBox.comboBox.SelectedIndex = groupsGroupBox.comboBox.FindString(user.UserGroup);
            groupsGroupBox.comboBox.SelectedIndexChanged += TextBox_TextChanged;

            if (user.UserAdmin == "1") checkBoxState = true;
            adminCheckBox.Checked = checkBoxState;
            adminCheckBox.CheckedChanged += TextBox_TextChanged;

            ButtonVisibleFalse();

            ActiveControl = nameGroupBox.textBox1;
            nameGroupBox.textBox1.SelectionStart = 0;

            ShowDialog();
        }
        private void ButtonVisibleFalse()
        {
            button.Visible = false;
            panel.Height = panel.Height - 40 - 40;
            ClientSize = new Size(panel.Width, panel.Height);
            errorLabel.Text = "";
        }
        private void ButtonVisibleTrue()
        {
            button.Visible = true;
            panel.Height = panel.Height + 40 + 40;
            ClientSize = new Size(panel.Width, panel.Height);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (nameGroupBox.textBox1.Text == user.UserName &&
                nameGroupBox.textBox2.Text == user.UserMiddleName &&
                lastNameGroupBox.textBox.Text == user.UserLastName &&
                (passwordGroupBox.textBox.Text == "" || passwordGroupBox.textBox.Text == Vars.Password) &&
                phoneNumberGroupBox.maskedTextBox.Text == user.UserPhoneNumber &&
                eMailGroupBox.textBox.Text == user.UserEmail &&
                addressGroupBox.textBox.Text == user.UserAddress &&
                groupsGroupBox.comboBox.Text == user.UserGroup &&
                adminCheckBox.Checked == checkBoxState)
            {
                if (!change) return; 
                ButtonVisibleFalse(); change = false; return;
            }
            if (change) return;
            ButtonVisibleTrue(); change= true; return;
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

            user.UserPassword = PasswordHash.HashCode(passwordGroupBox.textBox.Text);
            if (adminCheckBox.Checked) user.UserAdmin = "1";
            else user.UserAdmin = "0";
            user.UserName = nameGroupBox.textBox1.Text;
            user.UserMiddleName = nameGroupBox.textBox2.Text;
            user.UserLastName = lastNameGroupBox.textBox.Text;
            user.UserEmail = eMailGroupBox.textBox.Text;
            user.UserPhoneNumber = phoneNumberGroupBox.maskedTextBox.Text;
            user.UserAddress = addressGroupBox.textBox.Text;
            user.UserGroup = groupsGroupBox.comboBox.Text;

            if (!UserData.UsersTableActions.ChangeUserData(user)) return;

            UserDataChanged = true;

            Close();
        }
    }
}
