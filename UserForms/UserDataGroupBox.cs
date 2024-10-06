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
        private MaskedTextBox maskedTextBox;
        private TextBox textBox, textBox2;
        private string watermarkText, watermark2Text;
        bool textBoxWatermarkTextKeyPressed = false;
        bool textBox2WatermarkTextKeyReleased = false;

        public UserDataGroupBox(string groupBoxText, int X, int Y,
            Image userImage, int textBoxMaxLength, string textBoxWatermarkText = "",
            bool hidenTextBoxText = false, bool mask = false, bool twoTextBoxes = false,
            string textBox2WatermarkText = "")
        {
            watermarkText = textBoxWatermarkText;
            watermark2Text = textBox2WatermarkText;

            ClientSize = new Size(303, 50);
            Text = groupBoxText;
            Location = new Point(X, Y);

            label = new Label()
            {
                AutoSize = false,
                Size = new Size(32, 32),
                Location = new Point(2, 15),
                //TextAlign = ContentAlignment.MiddleRight,
                //Font = new Font("Calibri", 15, FontStyle.Regular),
                Image = userImage
            };
            Controls.Add(label);

            if (!mask)
            {
                textBox = new TextBox()
                {
                    Name = "textBox",
                    Font = new Font("Calibri", 15, FontStyle.Regular),
                    Text = textBoxWatermarkText,
                    MaxLength = textBoxMaxLength,
                    Width = 267,
                    Location = new Point(34, 15),
                    ForeColor=SystemColors.GrayText
                };

                if (hidenTextBoxText)
                    textBox.PasswordChar = '*';

                Controls.Add(textBox);
                textBox.KeyPress += TextBox_KeyPress;
                textBox.LostFocus += TextBox_LostFocus;

                if (twoTextBoxes)
                {
                    textBox2 = new TextBox()
                    {
                        Name = "textBox2",
                        Font = new Font("Calibri", 15, FontStyle.Regular),
                        Text = textBox2WatermarkText,
                        MaxLength = textBoxMaxLength,
                        Width = 133,
                        Location = new Point(168, 15),
                        ForeColor = SystemColors.GrayText
                    };
                    textBox.Width = 133;
                    Controls.Add (textBox2);
                    textBox2.KeyPress += TextBox2_KeyPress;
                    textBox2.LostFocus += TextBox2_LostFocus;
                }

            }
            else
            {
                maskedTextBox = new MaskedTextBox()
                {
                    Name = "maskedTextBox",
                    Mask = "+0(000) 000-00-00",
                    Font = new Font("Calibri", 15, FontStyle.Regular),
                    Width = 267,
                    Location = new Point(34, 15)
                };
                Controls.Add(maskedTextBox);
                maskedTextBox.TextChanged += MaskedTextBox_TextChanged;
            }
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
        private void TextBox2_LostFocus(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = watermark2Text;
                textBox2WatermarkTextKeyReleased = false;
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
        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox2.ForeColor = SystemColors.WindowText;
            if (textBox2.Text == watermark2Text && !textBox2WatermarkTextKeyReleased)
            {                                       
                textBox2.Text = "";
                textBox2WatermarkTextKeyReleased = true;
            }
            if (e.KeyChar.ToString() == "\b" && textBox2.Text.Length == 1)
            {
                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = watermark2Text;
                textBox2WatermarkTextKeyReleased = false;
            }
        }
        private void MaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            maskedTextBox.ForeColor = SystemColors.WindowText;
        }
    }
}
