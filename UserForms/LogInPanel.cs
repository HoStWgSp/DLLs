using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WorkWithUser.UserForms
{
    internal class LogInPanel : MainPanel
    {
        Label loginLabel;

        TextBox userNameTextBox,
            userPasswordTextBox;


        public LogInPanel(MainForm form):base(form)
        {
            loginLabel = new Label()
            {
                Text = "Авторизация",
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(200, 30),
                //BackColor=Color.DarkBlue
            };
            loginLabel.Location = new Point(form.ClientSize.Width / 2 - loginLabel.Width / 2, 5);
            userNameTextBox = new TextBox()
            {
                Width = 300,
                Font = new Font("Calibri", 15, FontStyle.Regular),
            };
            userNameTextBox.Location = new Point(form.ClientSize.Width / 2 - userNameTextBox.Width / 2, loginLabel.Location.Y + loginLabel.Height + 10);

            Controls.Add(loginLabel);
            Controls.Add(userNameTextBox);
        }
    }
}
