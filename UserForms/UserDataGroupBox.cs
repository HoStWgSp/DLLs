using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWithUser.UserForms
{
    internal class UserDataGroupBox : GroupBox
    {
        private Label label;
        private TextBox textBox;
        private string watermarkText;
        bool textBoxWatermarkTextKeyPressed = false;

        public UserDataGroupBox(string groupBoxText, int X, int Y, Image userImage, int textBoxMaxLength, string textBoxWatermarkText = "", bool hidenTextBoxText = false)
        {
            watermarkText = textBoxWatermarkText;

            ClientSize = new Size(303, 50);
            Text = groupBoxText;
            Location = new Point(X, Y);

            textBox = new TextBox() 
            { 
                Name = "textBox", 
                Font = new Font("Calibri", 15, FontStyle.Regular),
                Text = textBoxWatermarkText,
                MaxLength = textBoxMaxLength
            };

            if (textBoxWatermarkText != "")
                textBox.ForeColor = SystemColors.GrayText;

            if (hidenTextBoxText)
                textBox.PasswordChar = '*';

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

            textBox.KeyPress += TextBox_KeyPress;
            textBox.LostFocus += TextBox_LostFocus;
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            if (textBox.Text == "")
            {
                textBox.ForeColor = SystemColors.GrayText;
                textBox.Text = watermarkText;
                textBoxWatermarkTextKeyPressed = false;
            }
        }
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.ForeColor = SystemColors.WindowText;
            if (textBox.Text == watermarkText && !textBoxWatermarkTextKeyPressed)
            {
                textBox.Text = "";
                textBoxWatermarkTextKeyPressed = true;
            }
            if (e.KeyChar.ToString() == "\b" && textBox.Text.Length == 1)
            {
                textBox.ForeColor = SystemColors.GrayText;
                textBox.Text = watermarkText;
                textBoxWatermarkTextKeyPressed = false;
            }
        }
    }
}
