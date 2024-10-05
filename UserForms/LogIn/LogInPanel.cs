using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WorkWithUser.UserForms.LogIn
{
    internal class LogInPanel : MainPanel
    {
        Label loginLabel;

        GroupBox userGroupBox,
            passwordGroupBox;

        Button logInButton;

        MainForm mainForm;


        public LogInPanel(MainForm mainForm, Image userImage, Image passwordImage) :base(mainForm)
        {
            this.mainForm = mainForm;

            loginLabel = new Label()
            {
                Text = Vars.Authorization,
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(200, 30)
            };
            loginLabel.Location = new Point(mainForm.ClientSize.Width / 2 - loginLabel.Width / 2, 5);

            userGroupBox = new UserPassGroupBox(mainForm,
                Vars.User + ":",
                loginLabel.Location.Y + loginLabel.Height + 10,
                userImage, Vars.NameAndLastName
                );

            passwordGroupBox = new UserPassGroupBox(mainForm,
                Vars.Password + ":",
                userGroupBox.Location.Y + userGroupBox.Height + 10,
                passwordImage
                );

            logInButton = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 50),
                Text = Vars.Enter,
                TextAlign = ContentAlignment.MiddleCenter
            };
            logInButton.Location = new Point(mainForm.ClientSize.Width / 2 - logInButton.Width / 2, passwordGroupBox.Location.Y + passwordGroupBox.Height + 10);
            logInButton.Click += LogInButton_Click;

            Controls.Add(loginLabel);
            Controls.Add(userGroupBox);
            Controls.Add(passwordGroupBox);
            Controls.Add(logInButton);
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            UserName = userGroupBox.Controls["textBox"].Text;
            UserPassword = passwordGroupBox.Controls["textBox"].Text;
            mainForm.Close();
        }
    }
}
