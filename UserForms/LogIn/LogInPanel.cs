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

        public LogInPanel(MainForm form, Image userImage, Image passwordImage):base(form)
        {
            loginLabel = new Label()
            {
                Text = Vars.Authorization,
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(200, 30)
            };
            loginLabel.Location = new Point(form.ClientSize.Width / 2 - loginLabel.Width / 2, 5);

            userGroupBox = new UserPassGroupBox(form,
                Vars.User + ":",
                loginLabel.Location.Y + loginLabel.Height + 10,
                userImage
                );

            passwordGroupBox = new UserPassGroupBox(form,
                Vars.Password + ":",
                userGroupBox.Location.Y + userGroupBox.Height + 10,
                passwordImage
                );

            Controls.Add(loginLabel);
            Controls.Add(userGroupBox);
            Controls.Add(passwordGroupBox);
        }
    }
}
