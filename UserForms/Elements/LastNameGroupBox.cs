using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserData.UserForms.Elements.Abstracts;


namespace UserData.UserForms.Elements
{
    internal class LastNameGroupBox : GroupBoxTextBox 
    {
        public LastNameGroupBox(int X, int Y) :
            base(Vars.LastName + ":", Properties.Resources.User, X, Y, Vars.LastName, Vars.UserTextL)
        {

        }
        //private string watermarkText;
        //private Label label;
        //internal TextBox textBox;
        //private bool textBoxWatermarkTextKeyPressed = false;


        //public GroupBoxTextBox(string groupBoxText, Image image, int X, int Y, string textBoxWatermarkText, int textBoxMaxLength)
        //{
        //    watermarkText = textBoxWatermarkText;

        //    ClientSize = new Size(303, 50);
        //    Text = groupBoxText;
        //    Location = new Point(X, Y);
        //    Name = groupBoxText;

        //    label = new Label()
        //    {
        //        AutoSize = false,
        //        Size = new Size(32, 32),
        //        Location = new Point(2, 15),
        //        Image = image,
        //        Name = "label"
        //    };
        //    Controls.Add(label);

        //    textBox = new TextBox()
        //    {
        //        Name = "textBox",
        //        Font = new Font("Calibri", 15, FontStyle.Regular),
        //        Text = textBoxWatermarkText,
        //        MaxLength = textBoxMaxLength,
        //        Width = 267,
        //        Location = new Point(34, 15),
        //        ForeColor = SystemColors.GrayText
        //    };
        //    Controls.Add(textBox);
        //    textBox.KeyPress += TextBox_KeyPress;
        //    textBox.LostFocus += TextBox_LostFocus;
        //}
        //internal void TextBox_LostFocus(object sender, EventArgs e)
        //{
        //    if (textBox.Text == "")
        //    {
        //        textBox.ForeColor = SystemColors.GrayText;
        //        textBox.Text = watermarkText;
        //        textBoxWatermarkTextKeyPressed = false;
        //    }
        //}
        //internal void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    textBox.ForeColor = SystemColors.WindowText;
        //    if (textBox.Text == watermarkText && !textBoxWatermarkTextKeyPressed)
        //    {
        //        textBox.Text = "";
        //        textBoxWatermarkTextKeyPressed = true;
        //    }
        //    if (e.KeyChar.ToString() == "\b" && textBox.Text.Length == 1)
        //    {
        //        textBox.ForeColor = SystemColors.GrayText;
        //        textBox.Text = watermarkText;
        //        textBoxWatermarkTextKeyPressed = false;
        //    }
        //}

        /// <summary>
        /// Проверяет заполнен ли TextBox. Если не заполнен возвращает true
        /// </summary>
        internal bool TextBoxEmpty { get { return Checks.TextBoxEmptyOrWaterMarked(textBox, Vars.LastName); } }
    }
}
