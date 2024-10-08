using System.Windows.Forms;
using System.Drawing;
using System;
using System.Net.Mail;

namespace WorkWithUser.UserForms
{
    public class UserMainPanel : Panel
    {
        internal Label loginLabel,
            errorLabel,
            registrationLabel;
        public UserDataGroupBox userGroupBox,
            userGroupBox2,
            passwordGroupBox,
            eMailGroupBox,
            phoneGroupBox,
            groupGroupBox;
        internal Button button;


        public UserMainPanel(User user)
        {
            loginLabel = new Label()
            {
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(300, 30),
                Location = new Point(42, 5)
            };

            userGroupBox = new UserDataGroupBox(
                Vars.User + ":", 41, 45,
                Properties.Resources.User, Vars.UserTextL, false, Vars.NameAndLastName);
            userGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;

            userGroupBox2 = new UserDataGroupBox(
                Vars.User + ":", 41, 45,
                Properties.Resources.User, Vars.UserTextL,
                false, Vars.Name, true, Vars.LastName);
            userGroupBox2.Controls["textBox"].KeyDown += TextBox_KeyDown;
            userGroupBox2.Controls["textBox2"].KeyDown += TextBox_KeyDown;

            passwordGroupBox = new UserDataGroupBox(
                Vars.Password + ":", 41, 105,
                Properties.Resources.Password, Vars.UserTextL, true, Vars.Password);
            passwordGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;

            eMailGroupBox = new UserDataGroupBox(
                Vars.EMail + ":", 41, 165,
                Properties.Resources.EMail, Vars.UserEMailL, false, Vars.EMail);
            eMailGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;

            phoneGroupBox = new UserDataGroupBox(
                Vars.Phone + ":", 41, 225,
                Properties.Resources.Phone, "+0(000) 000-00-00");
            phoneGroupBox.Controls["maskedTextBox"].KeyDown += TextBox_KeyDown;

            groupGroupBox = new UserDataGroupBox(
                Vars.Group + ":", 41, 285,
                Properties.Resources.Group, user.groupList);
            groupGroupBox.Controls["comboBox"].KeyDown += TextBox_KeyDown;

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

            button = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 40),
                Text = Vars.Enter,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(92, 355)
            };
            button.Click += Button_Click;

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


        }

        internal virtual bool UserTextBoxOK()
        {
            if (!userGroupBox.TextBoxEmpty) return true;
            userGroupBox.Controls["textBox"].ForeColor = Color.Red;
            errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.User + "!";
            return false;
        }
        internal bool PasswordTextBoxOK()
        {
            if (!passwordGroupBox.TextBoxEmpty) return true;
            passwordGroupBox.Controls["textBox"].ForeColor = Color.Red;
            errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Password + "!";
            return false;
        }
        internal bool EMailTextBoxOK()
        {
            if (eMailGroupBox.TextBoxEmpty)
            {
                eMailGroupBox.Controls["textBox"].ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Password + "!";
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

        private void RegistrationLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void RegistrationLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        internal virtual void RegistrationLabel_Click(object sender, System.EventArgs e) { }

        internal virtual void Button_Click(object sender, System.EventArgs e) { }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button.PerformClick();
            }
        }
    }
}
