using System.Windows.Forms;
using System.Drawing;
using System;
using System.Net.Mail;

namespace WorkWithUser.UserForms
{
    public class UserMainPanel : Panel
    {
        internal User user;

        internal Label loginLabel,
            errorLabel,
            registrationLabel;
        public UserDataGroupBox userGroupBox,
            passwordGroupBox,
            eMailGroupBox,
            phoneGroupBox,
            groupGroupBox;
        internal Button button;


        public UserMainPanel(User user)
        {
            this.user = user;

            loginLabel = new Label()
            {
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(300, 30),
                Location = new Point(42, 5)
            };   

            passwordGroupBox = new UserDataGroupBox(
                Vars.Password + ":", 41, 105,
                Properties.Resources.Password, Vars.UserTextL, Vars.Password);
            passwordGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;

            eMailGroupBox = new UserDataGroupBox(
                Vars.EMail + ":", 41, 165,
                Properties.Resources.EMail, Vars.UserEMailL, Vars.EMail);
            eMailGroupBox.Controls["textBox"].KeyDown += TextBox_KeyDown;

            phoneGroupBox = new UserDataGroupBox(
                Vars.Phone + ":", 41, 225,
                Properties.Resources.Phone, "+0(000) 000-00-00");
            phoneGroupBox.Controls["maskedTextBox"].KeyDown += TextBox_KeyDown;


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
            UserActions.UserData(user, Vars.Registration);
            user.userForm.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - user.userForm.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - user.userForm.Height) / 2);
        }

        internal virtual void Button_Click(object sender, System.EventArgs e) { }

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
