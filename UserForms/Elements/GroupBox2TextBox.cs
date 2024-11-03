using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserData.UserForms.Elements
{
    internal class GroupBox2TextBox : GroupBox
    {
        private Label label;
        public TextBox textBox, textBox2;
        private string watermarkText, watermark2Text;
        private bool textBoxWatermarkTextKeyPressed = false;
        private bool textBox2WatermarkTextKeyReleased = false;

        public GroupBox2TextBox(string groupBoxText, Image image, int X, int Y,
            string textBoxWatermarkText, string textBox2WatermarkText, int textBoxMaxLength)
        {
            watermarkText = textBoxWatermarkText;
            watermark2Text = textBox2WatermarkText;

            ClientSize = new Size(303, 50);
            Text = groupBoxText;
            Location = new Point(X, Y);
            Name = groupBoxText;

            label = new Label()
            {
                AutoSize = false,
                Size = new Size(32, 32),
                Location = new Point(2, 15),
                Image = image,
                Name = "label"
            };
            Controls.Add(label);

            textBox = new TextBox()
            {
                Name = "textBox",
                Font = new Font("Calibri", 15, FontStyle.Regular),
                Text = textBoxWatermarkText,
                MaxLength = textBoxMaxLength,
                Width = 133,
                Location = new Point(34, 15),
                ForeColor = SystemColors.GrayText
            };
            Controls.Add(textBox);
            textBox.KeyPress += TextBox_KeyPress;
            textBox.LostFocus += TextBox_LostFocus;

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
            Controls.Add(textBox2);
            textBox2.KeyPress += TextBox2_KeyPress;
            textBox2.LostFocus += TextBox2_LostFocus;
        }
        internal void TextBox_LostFocus(object sender, EventArgs e)
        {
            if (textBox.Text == "")
            {
                textBox.ForeColor = SystemColors.GrayText;
                textBox.Text = watermarkText;
                textBoxWatermarkTextKeyPressed = false;
            }
        }
        internal void TextBox2_LostFocus(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = watermark2Text;
                textBox2WatermarkTextKeyReleased = false;
            }
        }
        internal void TextBox_KeyPress(object sender, KeyPressEventArgs e)
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
        internal void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
