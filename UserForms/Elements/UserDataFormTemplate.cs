using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserData.MyElements;

namespace UserData.UserForms.Elements
{
    internal class UserDataFormTemplate : Form
    {
        string[] groupsList {  get; set; }

        private Panel panel;

        internal Label loginLabel,
            errorLabel;
        internal GroupBox2TextBox nameGroupBox;
        internal GroupBoxTextBox lastNameGroupBox, passwordGroupBox, eMailGroupBox, addressGroupBox;
        internal GroupBoxMaskedTextBox phoneNumberGroupBox;
        internal GroupBoxComboBox groupsGroupBox;
        internal CheckBox adminCheckBox;
        internal Button button;

        public UserDataFormTemplate (Icon formIcon, string[] groupsList, string labelText, string buttonText, bool addAdmin)
        {
            this.groupsList = groupsList;
            Icon = formIcon;
            StartPosition = FormStartPosition.CenterParent;
            AutoSize = false;
            MinimizeBox = false;
            MaximizeBox = false;

            loginLabel = new Label()
            {
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(300, 30),
                Location = new Point(42, 5),
                Text = labelText
            };

            nameGroupBox = new GroupBox2TextBox(Vars.Name + ", " + Vars.MiddleName + ":", Properties.Resources.User, 41, 45,
                Vars.Name, Vars.MiddleName, Vars.UserTextL);

            lastNameGroupBox = new GroupBoxTextBox(Vars.LastName + ":", Properties.Resources.User, 41, 105,
                Vars.LastName, Vars.UserTextL);

            passwordGroupBox = new GroupBoxTextBox(Vars.Password + ":", Properties.Resources.Password,
                41, 165, Vars.Password, Vars.UserTextL);

            phoneNumberGroupBox = new GroupBoxMaskedTextBox(Vars.Phone + ":", Properties.Resources.Phone, 41, 225, "+0(000) 000-00-00");

            eMailGroupBox = new GroupBoxTextBox(Vars.EMail + ":", Properties.Resources.EMail, 41, 285,
                Vars.EMail, Vars.UserEMailL);

            addressGroupBox = new GroupBoxTextBox(Vars.Address, Properties.Resources.Address, 41, 345,
                Vars.Address, 500);

            groupsGroupBox = new GroupBoxComboBox(Vars.Group, Properties.Resources.Group, 41, 405,
                groupsList);
            groupsGroupBox.comboBox.Enabled = false;

            errorLabel = new Label()
            {
                AutoSize = false,
                Name = "errorLabel",
                Size = new Size(303, 12),
                Font = new Font("Calibri", 8, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Location = new Point(41, 455)
            };

            button = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 40),
                Text = buttonText,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(92, 475)
            };
            button.Click += Button_Click;

            panel = new Panel();
            panel.Size = new Size(384, 545);

            if (addAdmin)
            {
                adminCheckBox = new CheckBox()
                {
                    Name = "checkBox",
                    Text = Vars.Administrator,
                    Width = groupsGroupBox.comboBox.Width
                };
                Controls.Add(adminCheckBox);


                adminCheckBox.Location = new Point(groupsGroupBox.Location.X + groupsGroupBox.Controls["label"].Width + 2,
                    groupsGroupBox.Location.Y + groupsGroupBox.Height);

                errorLabel.Location = new Point(errorLabel.Location.X, errorLabel.Location.Y + adminCheckBox.Height);
                button.Location = new Point(button.Location.X, button.Location.Y + adminCheckBox.Height);
                panel.Height = panel.Height + adminCheckBox.Height;

                groupsGroupBox.comboBox.Enabled = true;
            }

            panel.Controls.Add(loginLabel);
            panel.Controls.Add(nameGroupBox);
            panel.Controls.Add(lastNameGroupBox);
            panel.Controls.Add(passwordGroupBox);
            panel.Controls.Add(phoneNumberGroupBox);
            panel.Controls.Add(eMailGroupBox);
            panel.Controls.Add(addressGroupBox);
            panel.Controls.Add(groupsGroupBox);
            panel.Controls.Add(errorLabel);
            panel.Controls.Add(button);

            Controls.Add(panel);

            ClientSize = new Size(panel.Width, panel.Height);

            nameGroupBox.textBox.KeyDown += TextBox_KeyDown;
            nameGroupBox.textBox2.KeyDown += TextBox_KeyDown;
            lastNameGroupBox.textBox.KeyDown += TextBox_KeyDown;
            passwordGroupBox.textBox.KeyDown += TextBox_KeyDown;
            phoneNumberGroupBox.maskedTextBox.KeyDown += TextBox_KeyDown;
            eMailGroupBox.textBox.KeyDown += TextBox_KeyDown;
            addressGroupBox.textBox.KeyDown += TextBox_KeyDown;
            groupsGroupBox.comboBox.KeyDown += TextBox_KeyDown;

            nameGroupBox.textBox.SelectionStart = 0;
        }
        internal void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button.PerformClick();
            }
        }
        internal bool TextBoxesNotEmptyOrWaterMarked()
        {
            if (!Checks.TextBoxNotEmptyOrWaterMarked(nameGroupBox.textBox, errorLabel, Vars.Name, Vars.TextBoxEmpty + " " + Vars.Name + "!")) return false;
            if (!Checks.TextBoxNotEmptyOrWaterMarked(nameGroupBox.textBox2, errorLabel, Vars.MiddleName, Vars.TextBoxEmpty + " " + Vars.MiddleName + "!")) return false;
            if (!Checks.TextBoxNotEmptyOrWaterMarked(lastNameGroupBox.textBox,errorLabel, Vars.LastName, Vars.TextBoxEmpty + " " + Vars.LastName + "!")) return false;
            if (!PhoneMaskedTextBoxOK()) return false;
            if (!EMailTextBoxOK()) return false;
            if (!Checks.TextBoxNotEmptyOrWaterMarked(addressGroupBox.textBox, errorLabel, Vars.Address, Vars.TextBoxEmpty + " " + Vars.Address + "!")) return false;
            if (!GroupComboBoxOK()) return false;
            errorLabel.Text = "";
            return true;
        }
        private bool PhoneMaskedTextBoxOK()
        {
            if (phoneNumberGroupBox.maskedTextBox.MaskCompleted &&
                phoneNumberGroupBox.maskedTextBox.Text != "+ (   )    -  -") return true;
            phoneNumberGroupBox.maskedTextBox.ForeColor = Color.Red;
            errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.Phone + "!";
            return false;
        }
        private bool EMailTextBoxOK()
        {
            if (eMailGroupBox.textBox.Text == Vars.EMail ||
                eMailGroupBox.textBox.Text == "")
            {
                eMailGroupBox.textBox.ForeColor = Color.Red;
                errorLabel.Text = Vars.TextBoxEmpty + " " + Vars.EMail + "!";
                return false;
            }
            else
            {
                try
                {
                    MailAddress addr = new MailAddress(eMailGroupBox.textBox.Text);
                }
                catch
                {
                    eMailGroupBox.textBox.ForeColor = Color.Red;
                    errorLabel.Text = Vars.EmailIncorrect + "!";
                    return false;
                }
            }
            return true;
        }
        private bool GroupComboBoxOK()
        {
            if (groupsGroupBox.comboBox.Text != "" &&
                groupsGroupBox.comboBox.Text != null) return true;
            errorLabel.Text = Vars.GroupNotChoosen;
            return false;
        }
        internal virtual void Button_Click(object sender, EventArgs e) { }
    
    }
}
