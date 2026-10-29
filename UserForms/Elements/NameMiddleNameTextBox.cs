using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserData.UserForms.Elements
{
    internal class NameMiddleNameTextBox : GroupBox
    {
        private Label label;
        private TextBox textBox1, textBox2;
        private string watermark1Text, watermark2Text;
        private bool textBox1WatermarkTextKeyPressed = false;
        private bool textBox2WatermarkTextKeyReleased = false;

        public NameMiddleNameTextBox(int X, int Y)
        {
            watermark1Text = Vars.Name;
            watermark2Text = Vars.MiddleName;

            ClientSize = new Size(303, 50);
            Text = Vars.Name + ", " + Vars.MiddleName + ":";
            Name = Text;
            Location = new Point(X, Y);

            label = new Label()
            {
                AutoSize = false,
                Size = new Size(32, 32),
                Location = new Point(2, 15),
                Image = Properties.Resources.User,
                Name = "label"
            };
            Controls.Add(label);

            textBox1 = new TextBox()
            {
                Name = "textBox",
                Font = new Font("Calibri", 15, FontStyle.Regular),
                Text = watermark1Text,
                MaxLength = Vars.UserTextL,
                Width = 133,
                Location = new Point(34, 15),
                ForeColor = SystemColors.GrayText
            };
            Controls.Add(textBox1);
            textBox1.KeyPress += TextBox_KeyPress;
            textBox1.LostFocus += TextBox_LostFocus;

            textBox2 = new TextBox()
            {
                Name = "textBox2",
                Font = new Font("Calibri", 15, FontStyle.Regular),
                Text = watermark2Text,
                MaxLength = Vars.UserTextL,
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
            if (textBox1.Text == "")
            {
                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = watermark1Text;
                textBox1WatermarkTextKeyPressed = false;
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
            textBox1.ForeColor = SystemColors.WindowText;
            if (textBox1.Text == watermark1Text && !textBox1WatermarkTextKeyPressed)
            {
                textBox1.Text = "";
                textBox1WatermarkTextKeyPressed = true;
            }
            if (e.KeyChar.ToString() == "\b" && textBox1.Text.Length == 1)
            {
                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = watermark1Text;
                textBox1WatermarkTextKeyPressed = false;
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

        /// <summary>
        /// Проверяет заполнен ли TextBox. Если не заполнен возвращает true
        /// </summary>
        internal bool TextBox1Comlete { get { return Checks.TextBoxComplete(textBox1, Vars.Name); } }
        internal string TextBox1Text { get { return textBox1.Text; } set { textBox1.Text = value; } }
        internal string TextBox2Text 
        {
            get 
            {
                if (textBox2.Text == watermark2Text || textBox2.Text == null || textBox2.Text == "") 
                    return "";
                return textBox2.Text; 
            }
            set { textBox2.Text = value; }
        }
        internal int TextBox1SelectionStart { set { textBox1.SelectionStart = value; } }
        internal TextBox TextBox1ActiveControl { get { return textBox1; } }
    }
}
