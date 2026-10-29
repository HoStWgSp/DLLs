using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UserData.UserForms.Elements.Abstracts
{
    internal abstract class GroupBoxTextBox : GroupBox
    {
        private string watermarkText;
        private Label label;
        private TextBox textBox;
        private bool textBoxWatermarkTextKeyPressed = false;
        public GroupBoxTextBox(string groupBoxText, Image image, int X, int Y, string textBoxWatermarkText, int textBoxMaxLength)
        {
            watermarkText = textBoxWatermarkText;

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
                Width = 267,
                Location = new Point(34, 15),
                ForeColor = SystemColors.GrayText
            };
            Controls.Add(textBox);
            textBox.KeyPress += TextBox_KeyPress;
            textBox.LostFocus += TextBox_LostFocus;
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
        /// <summary>
        /// Проверяет заполнена ли строка
        /// </summary>
        internal bool TextBoxComplete { get { return Checks.TextBoxComplete(textBox, watermarkText); } }

        internal string TextBoxText {  get { return textBox.Text; } set { textBox.Text = value; } }
        internal Color TextBoxTextColor { set { textBox.ForeColor = value; } }
        internal int SelectionStart { set { textBox.SelectionStart = value; } }
        internal char PasswordChar {  set { textBox.PasswordChar = value; } }
        internal TextBox TextBoxActiveControl { get { return textBox; } } 
    }
}
