using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWithUser.UserForms.LogIn
{
    internal class UserPassGroupBox : GroupBox
    {
        private Label label;
        private TextBox textBox;

        public UserPassGroupBox(MainForm form, string groupBoxText, int Y, Image userImage)
        {
            ClientSize = new Size(303, 50);
            Text = groupBoxText;
            Location= new Point(form.ClientSize.Width / 2 - Width / 2, Y);

            textBox = new TextBox() { Name = "textBox", Font = new Font("Calibri", 15, FontStyle.Regular) };

            if (userImage != null)
            {
                label = new Label()
                {
                    AutoSize = false,
                    Size = new Size(32, 32),
                    Location = new Point(2, 15),
                    TextAlign = ContentAlignment.MiddleRight,
                    Font = new Font("Calibri", 15, FontStyle.Regular)
                };
                Controls.Add(label);
                label.Image = userImage;
                textBox.Width = ClientSize.Width - label.Width - label.Location.X - 2;
                textBox.Location = new Point(label.Width + label.Location.X, 15);
            }
            else
            {
                textBox.Width = ClientSize.Width - 4;
                textBox.Location = new Point(2, 15);
            }

            Controls.Add(textBox);
        }
    }
}
