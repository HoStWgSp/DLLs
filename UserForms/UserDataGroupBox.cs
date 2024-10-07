using System;
using System.Drawing;
using System.Windows.Forms;

namespace WorkWithUser.UserForms
{
    internal class UserDataGroupBox : GroupBox
    {
        private Label label;
        private MaskedTextBox maskedTextBox;
        private TextBox textBox, textBox2;
        private ComboBox comboBox;
        private string watermarkText, watermark2Text;
        bool textBoxWatermarkTextKeyPressed = false;
        bool textBox2WatermarkTextKeyReleased = false;

        /// <summary>
        /// Создает GroupBox с TextBox
        /// </summary>
        /// <param name="groupBoxText"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="labelImage"></param>
        /// <param name="textBoxMaxLength"></param>
        /// <param name="hidenTextBoxText"></param>
        /// <param name="textBoxWatermarkText"></param>
        /// <param name="twoTextBoxes"></param>
        /// <param name="textBox2WatermarkText"></param>
        public UserDataGroupBox(string groupBoxText, int X, int Y, Image labelImage,
            int textBoxMaxLength, bool hidenTextBoxText = false,
            string textBoxWatermarkText = "", bool twoTextBoxes = false,
            string textBox2WatermarkText = "")
        {
            MajorInits(groupBoxText, X, Y, labelImage);

            watermarkText = textBoxWatermarkText;
            watermark2Text = textBox2WatermarkText;

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
                Controls.Add(textBox2);
                textBox2.KeyPress += TextBox2_KeyPress;
                textBox2.LostFocus += TextBox2_LostFocus;
            }
        }

        /// <summary>
        /// Создает GroupBox с MaskedTextBox
        /// </summary>
        /// <param name="groupBoxText"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="labelImage"></param>
        /// <param name="mask"></param>
        public UserDataGroupBox(string groupBoxText, int X, int Y, Image labelImage,
            string mask)
        {
            MajorInits(groupBoxText, X, Y, labelImage);

            maskedTextBox = new MaskedTextBox()
            {
                Name = "maskedTextBox",
                Mask = mask,
                Font = new Font("Calibri", 15, FontStyle.Regular),
                Width = 267,
                Location = new Point(34, 15)
            };
            Controls.Add(maskedTextBox);
            maskedTextBox.TextChanged += MaskedTextBox_TextChanged;
        }

        /// <summary>
        /// Создает GroupBox с ComboBox
        /// </summary>
        /// <param name="groupBoxText"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="labelImage"></param>
        /// <param name="groups"></param>
        public UserDataGroupBox(string groupBoxText, int X, int Y, Image labelImage,
            string[] groups)
        {
            MajorInits(groupBoxText, X, Y, labelImage);

            comboBox = new ComboBox()
            {
                Name = "comboBox",
                Font = new Font("Calibri", 15, FontStyle.Regular),
                Width = 267,
                Location = new Point(34, 15),
                DropDownStyle=ComboBoxStyle.DropDownList
            };
            comboBox.Items.AddRange(groups);
            Controls.Add(comboBox);
        }

        private void MajorInits(string groupBoxText, int X, int Y, Image labelImage)
        {
            ClientSize = new Size(303, 50);
            Text = groupBoxText;
            Location = new Point(X, Y);

            label = new Label()
            {
                AutoSize = false,
                Size = new Size(32, 32),
                Location = new Point(2, 15),
                Image = labelImage
            };
            Controls.Add(label);
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
