using System.Windows.Forms;
using System.Drawing;
using System;
using System.Net.Mail;
using UserData.MyElements;

namespace UserData.UserForms
{
    public class UserMainPanel : Panel
    {
        internal UserData userData;

        internal Label loginLabel,
            errorLabel,
            registrationLabel;
        internal UserGroupBoxes userGroupBox,
            passwordGroupBox,
            eMailGroupBox,
            phoneGroupBox,
            groupGroupBox;
        internal CheckBox adminCheckBox;
        internal Button button;


        //public UserMainPanel(UserData user, string labelText, string buttonText)
        //{
        //    Size = new Size(384, 241);
        //    this.user = user;
        //    LoginLabel(labelText);
        //    UserGroupBox(false);
        //    PasswordGroupBox(true);
        //    ErrorLabel();
        //    ButtonBody(buttonText);
        //}
        //public UserMainPanel(UserData user, string labelText, string adminCheckBoxText, string buttonText)
        //{
        //    Size = new Size(384, 405);
        //    this.user = user;
        //    LoginLabel(labelText);
        //    UserGroupBox(true);
        //    PasswordGroupBox(false);
        //    EMailGroupBox();
        //    PhoneGroupBox();
        //    ErrorLabel();
        //    ButtonBody(buttonText);

        //    if (user.Admin)
        //    {
        //        GroupGroupBox(true);
        //        AdminCheckBox(adminCheckBoxText);
        //        adminCheckBox.Location = new Point(groupGroupBox.Location.X + groupGroupBox.Controls["label"].Width + 2,
        //            groupGroupBox.Location.Y + groupGroupBox.Height);

        //        errorLabel.Location = new Point(errorLabel.Location.X, errorLabel.Location.Y + adminCheckBox.Height);
        //        button.Location = new Point(button.Location.X, button.Location.Y + adminCheckBox.Height);
        //        Height = Height + adminCheckBox.Height;
        //    }
        //    else GroupGroupBox(false);
        //}



        internal void LoginLabel(string labelText)
        {
            loginLabel = new Label()
            {
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(300, 30),
                Location = new Point(42, 5),
                Text = labelText
            };
            Controls.Add(loginLabel);
        }
        internal void UserGroupBox(bool twoTextBox)
        {
            if (!twoTextBox)
            {
                userGroupBox = new UserGroupBoxes(Vars.User + ":", "userGroupBox", 41, 45,
                Properties.Resources.User, Vars.NameAndLastName, Vars.UserTextL);
            }
            else
            {
                userGroupBox = new UserGroupBoxes(Vars.User + ":", "userGroupBox", 41, 45,
                Properties.Resources.User, Vars.Name, Vars.UserTextL, Vars.LastName);
                userGroupBox.Controls["textBox2"].KeyDown += TextBox_KeyDown;
            }
            userGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;
            Controls.Add(userGroupBox);
        }
        internal void PasswordGroupBox(bool PasswordChar)
        {
            passwordGroupBox = new UserGroupBoxes(
                Vars.Password + ":", "passwordGroupBox", 41, 105,
                Properties.Resources.Password, Vars.Password, Vars.UserTextL);
            if (PasswordChar) 
                (passwordGroupBox.Controls["textBox"] as TextBox).PasswordChar = '*';

            passwordGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;
            Controls.Add(passwordGroupBox);
        }
        internal void EMailGroupBox() 
        {
            eMailGroupBox = new UserGroupBoxes(
                Vars.EMail + ":", "eMailGroupBox", 41, 165,
                Properties.Resources.EMail, Vars.EMail, Vars.UserEMailL);
            eMailGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;
            Controls.Add(eMailGroupBox);
        }
        internal void PhoneGroupBox()
        {
            phoneGroupBox = new UserGroupBoxes(
                Vars.Phone + ":", "phoneGroupBox", 41, 225,
                Properties.Resources.Phone);
            phoneGroupBox.Controls["maskedTextBox"].KeyDown += TextBox_KeyDown;
            Controls.Add(phoneGroupBox);
        }
        internal void GroupGroupBox(bool comboBox)
        {
            if (comboBox)
            {
                groupGroupBox = new UserGroupBoxes(
                    Vars.Group + ":", "groupGroupBox", 41, 285,
                    Properties.Resources.Group, userData.groupsList);
                groupGroupBox.Controls["comboBox"].KeyDown += TextBox_KeyDown;
            }
            else
            {
                groupGroupBox = new UserGroupBoxes(
                Vars.EMail + ":", "groupGroupBox", 41, 285,
                Properties.Resources.Group, null, Vars.UserEMailL);
                (groupGroupBox.Controls["textBox"] as TextBox).ReadOnly = true;
            }
            Controls.Add(groupGroupBox);
        }
        internal void AdminCheckBox(string checkBoxText)
        {
            adminCheckBox = new CheckBox()
            {
                Name = "checkBox",
                Text = checkBoxText,
                Width = groupGroupBox.Controls["comboBox"].Width
            };
            Controls.Add(adminCheckBox);
        }
        internal void ErrorLabel()
        {
            errorLabel = new Label()
            {
                AutoSize = false,
                Name = "errorLabel",
                Size = new Size(303, 12),
                Font = new Font("Calibri", 8, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Location = new Point(41, 335)
            };
            Controls.Add(errorLabel);
        }
        internal void ButtonBody(string buttonText)
        {
            button = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 40),
                Text = buttonText,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(92, 355)
            };
            button.Click += Button_Click;
            Controls.Add(button);
        }
        internal void RegistrationLabel()
        {
            registrationLabel = new Label()
            {
                AutoSize = false,
                Size = new Size(200, 16),
                Text = Vars.GetRegistration,
                Font = new Font("Calibri", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.TopCenter,
                ForeColor = Color.Blue
            };
            registrationLabel.Click += RegistrationLabel_Click;
            registrationLabel.MouseEnter += RegistrationLabel_MouseEnter;
            registrationLabel.MouseLeave += RegistrationLabel_MouseLeave;
            registrationLabel.Location = new Point(92, 220);
            Controls.Add(registrationLabel);
        }

        internal bool UserTextBoxOK(bool twoTextBox = false)
        {
            if (twoTextBox)
            {
                if (userGroupBox.Controls["textBox"].Text == userGroupBox.watermarkText ||
                userGroupBox.Controls["textBox"].Text == "")
                {
                    userGroupBox.Controls["textBox"].ForeColor = Color.Red;
                    errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Name + "!";
                    return false;
                }
                if (userGroupBox.Controls["textBox2"].Text != userGroupBox.watermark2Text &&
                    userGroupBox.Controls["textBox2"].Text != "") return true;
                {
                    userGroupBox.Controls["textBox2"].ForeColor = Color.Red;
                    errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.LastName + "!";
                    return false;
                }
            }
            if (userGroupBox.Controls["textBox"].Text != userGroupBox.watermarkText &&
                userGroupBox.Controls["textBox"].Text != "") return true;
            {
                userGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.User + "!";
                return false;
            }
        }
        internal bool PasswordTextBoxOK()
        {
            if (passwordGroupBox.Controls["textBox"].Text != userGroupBox.watermarkText &&
                passwordGroupBox.Controls["textBox"].Text != "") return true;
            passwordGroupBox.Controls["textBox"].ForeColor = Color.Red;
            errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Password + "!";
            return false;
        }
        internal bool EMailTextBoxOK()
        {
            if (eMailGroupBox.Controls["textBox"].Text == eMailGroupBox.watermarkText ||
                eMailGroupBox.Controls["textBox"].Text == "")
            {
                eMailGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.EMail + "!";
                return false;
            }
            else
            {
                try
                {
                    var addr = new MailAddress(eMailGroupBox.Controls["textBox"].Text);
                }
                catch
                {
                    eMailGroupBox.Controls["textBox"].ForeColor = Color.Red;
                    errorLabel.Text = Vars.EmailIncorrect + "!";
                    return false;
                }
            }
            return true;
        }
        internal bool PhoneMaskedTextBoxOK()
        {
            if ((phoneGroupBox.Controls["maskedTextBox"] as MaskedTextBox).MaskCompleted &&
                phoneGroupBox.Controls["maskedTextBox"].Text != "+ (   )    -  -") return true;
            phoneGroupBox.Controls["maskedTextBox"].ForeColor = Color.Red;
            errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Phone + "!";
            return false;
        }
        internal bool GroupComboBoxOK()
        {
            if (groupGroupBox.Controls["comboBox"].Text != "" &&
                groupGroupBox.Controls["comboBox"].Text != null) return true;
            errorLabel.Text = Vars.GroupNotChoosen;
            return false;
        }

        internal void RegistrationLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        internal void RegistrationLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        internal void RegistrationLabel_Click(object sender, System.EventArgs e)
        {
            //userData.userActions.UserData(userData, Vars.Registration);
            //userData.userForm.Location = new Point(
            //    (Screen.PrimaryScreen.WorkingArea.Width - userData.userForm.Width) / 2,
            //    (Screen.PrimaryScreen.WorkingArea.Height - userData.userForm.Height) / 2);
        }

        internal virtual void Button_Click(object sender, EventArgs e) { }

        internal void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button.PerformClick();
            }
        }
    }
}
