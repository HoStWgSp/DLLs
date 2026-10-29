using System;
using System.Drawing;
using System.Windows.Forms;
using UserData.UserForms.Elements;

namespace UserData.UserForms
{
    internal class PasswordConfirmationForm : Form
    {
        public bool PasswordConfirmed { get; private set; } = false;
        public string Password { get; private set; }

        private Panel panel;

        private Label label, errorLabel;
        private PasswordGroupBox password1, password2;
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
            password1 = new PasswordGroupBox(41, 45);

            password1.TextBoxTextColor = SystemColors.WindowText;
            password1.TextBoxText = password;

            password2 = new PasswordGroupBox(41, 105);

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

            ActiveControl = password2.TextBoxActiveControl;
            password2.SelectionStart = 0;

            ShowDialog();
        }
        internal void Button_Click(object sender, EventArgs e)
        {
            if (password1.TextBoxText != password2.TextBoxText)
            {
                password1.TextBoxTextColor = Color.Red;
                password2.TextBoxTextColor = Color.Red;
                errorLabel.Text = Vars.PasswordsNotMached + "!";
            }
            else { Password = password1.TextBoxText; PasswordConfirmed = true; Close(); }
        }
    }
}
