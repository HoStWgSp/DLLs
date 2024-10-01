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

        TextBox userNameOrMailTextBox,
            userPasswordTextBox;


        public LogInPanel(MainForm form):base(form)
        {
            loginLabel = new Label()
            {
                Text = "Авторизация пользователя",
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(350, 40),
                BackColor=Color.DarkBlue
            };
            loginLabel.Location = new Point(form.Width / 2 - loginLabel.Width / 2, 5);
            userNameOrMailTextBox = new TextBox()
            {
                Location = new Point(20, loginLabel.Location.Y+ loginLabel.Height+10),

            };

            Controls.Add(loginLabel);
            Controls.Add(userNameOrMailTextBox);
        }
    }
}
