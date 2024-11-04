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
    internal class PasswordConfirmationForm : Form
    {
        public bool PasswordConfirmed { get; private set; } = false;
        public string Password { get; private set; }

        private Panel panel;

        private Label label, errorLabel;
        private GroupBoxTextBox password1, password2;
        private Button button;
        
        public PasswordConfirmationForm(string password, Icon formIcon)
        {
            Password = password;
            Icon = formIcon;
            StartPosition = FormStartPosition.CenterParent;
            AutoSize = false;
            MinimizeBox = false;
            MaximizeBox = false;

            label = new Label()
            {
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(300, 30),
                Location = new Point(42, 5),
                Text = Vars.PasswordConfirmation
            };
            password1 = new GroupBoxTextBox(Vars.Password + ":", Properties.Resources.Password, 41, 45,
                Vars.Password, Vars.UserTextL);

            password1.textBox.ForeColor = SystemColors.WindowText;
            password1.textBox.Text = password;

            password2 = new GroupBoxTextBox(Vars.PasswordConfirmation + ":", Properties.Resources.Password, 41, 105,
                Vars.PasswordConfirmation, Vars.UserTextL);

            errorLabel = new Label()
            {
                AutoSize = false,
                Name = "errorLabel",
                Size = new Size(303, 12),
                Font = new Font("Calibri", 8, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Location = new Point(41, 155)
            };

            button = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 40),
                Text = Vars.Confirm,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(92, 175)
            };
            button.Click += Button_Click;

            panel = new Panel();
            panel.Size = new Size(384, 241);

            panel.Controls.Add(label);
            panel.Controls.Add(password1);
            panel.Controls.Add(password2);
            panel.Controls.Add(errorLabel);
            panel.Controls.Add(button);

            Controls.Add(panel);

            ClientSize = new Size(panel.Width, panel.Height);

            ActiveControl = password2.textBox;
            password2.textBox.SelectionStart = 0;

            ShowDialog();
        }
        internal void Button_Click(object sender, EventArgs e)
        {
            if (Checks.TextBoxEmptyOrWaterMarked(password1.textBox, errorLabel, Vars.Password, Vars.TextBoxEmpty + " " + Vars.Password + "!")) return;
            if (Checks.TextBoxEmptyOrWaterMarked(password2.textBox, errorLabel, Vars.PasswordConfirmation, Vars.TextBoxEmpty + " " + Vars.PasswordConfirmation + "!")) return;

            if (password1.textBox.Text != password2.textBox.Text)
            {
                password1.textBox.ForeColor = Color.Red;
                password2.textBox.ForeColor = Color.Red;
                errorLabel.Text = Vars.PasswordsNotMached + "!";
            }
            else { Password = password1.textBox.Text; PasswordConfirmed = true; Close(); }
        }
    }
}
