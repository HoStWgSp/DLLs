using System;
using System.Drawing;
using System.Windows.Forms;

namespace WorkWithUser.MyElements
{
    internal class MyGroupBox : GroupBox
    {

        internal Label label;
        internal MaskedTextBox maskedTextBox;
        internal TextBox textBox, textBox2;
        internal ComboBox comboBox;
        internal CheckBox checkBox;
        internal string watermarkText, watermark2Text;
        bool textBoxWatermarkTextKeyPressed = false;
        bool textBox2WatermarkTextKeyReleased = false;
        public MyGroupBox(string groupBoxText, string groupBoxName, int X, int Y, 
            Image labelImage)
        {
            ClientSize = new Size(303, 50);
            Text = groupBoxText;
            Location = new Point(X, Y);
            Name = groupBoxName;


            label = new Label()
            {
                AutoSize = false,
                Size = new Size(32, 32),
                Location = new Point(2, 15),
                Image = labelImage,
                Name = "label"
            };
            Controls.Add(label);

        }

        internal void TextBoxCreate(string textBoxWatermarkText, int textBoxMaxLength)
        {
            textBox = new TextBox()
            {
                Name = "textBox",
                Font = new Font("Calibri", 15, FontStyle.Regular),
                Text = textBoxWatermarkText,
                MaxLength = textBoxMaxLength,
                Width = 267,
                Location = new Point(34, 15),
                ForeColor = SystemColors.GrayText
            };
            Controls.Add(textBox);
            textBox.KeyPress += TextBox_KeyPress;
            textBox.LostFocus += TextBox_LostFocus;
        }
        internal void MaskedTextBoxCreate()
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
        internal void ComboBoxCreate(string[] groups)
        {
            comboBox = new ComboBox()
            {
                Name = "comboBox",
                Font = new Font("Calibri", 15, FontStyle.Regular),
                Width = 267,
                Location = new Point(34, 15),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            comboBox.Items.AddRange(groups);
            Controls.Add(comboBox);
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
        private void MaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            maskedTextBox.ForeColor = SystemColors.WindowText;
        }
    }
}
