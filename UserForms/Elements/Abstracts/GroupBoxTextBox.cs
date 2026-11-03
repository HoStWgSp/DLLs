using System;
using System.Drawing;
using System.Windows.Forms;

namespace UserData.UserForms.Elements.Abstracts
{
    internal abstract class GroupBoxTextBox : GroupBox
    {
        /// <summary>
        /// Устанавливает заглавие GroupBox
        /// </summary>
        internal string GroupBoxText { set { Text = value; } }
        /// <summary>
        /// Устанавливает картинку перед TextBox
        /// </summary>
        internal Image LabelImage { set { label.Image = value; } }
        /// <summary>
        /// Устанавливает положение GroupBox
        /// </summary>
        internal Point GroupBoxLocation { set { Location = value; } }
        /// <summary>
        /// Устанавливает текст как водный знак в TextBox
        /// </summary>
        internal string WatermarkText { set { watermarkText = value; textBox.Text = value; } }
        private string watermarkText;
        /// <summary>
        /// Устанавливает максимальное количество символов в TextBox
        /// </summary>
        internal int TextBoxMaxLength { set { textBox.MaxLength  = value; } }
        /// <summary>
        /// Читает или записывает текст в/из TextBox
        /// </summary>
        internal string TextBoxText { get { return textBox.Text; } set { textBox.Text = value; } }
        /// <summary>
        /// Устанавливает цвет текста в TextBox
        /// </summary>
        internal Color TextBoxTextColor { set { textBox.ForeColor = value; } }
        /// <summary>
        /// Устанавливает положение курсора в TextBox при загрузке
        /// </summary>
        internal int SelectionStart { set { textBox.SelectionStart = value; } }
        /// <summary>
        /// Включает или выключает замену символов на *
        /// </summary>
        internal bool PasswordChar { set { if (value) passwordChar = '*'; else passwordChar = '\0'; } }
        private char passwordChar = '\0';
        /// <summary>
        /// Очистить TextBox
        /// </summary>
        internal bool TextBoxClear { set { if (value) TextBoxWatermarked(); } }

        internal TextBox TextBoxActiveControl { get { return textBox; } }


        private Label label;
        private TextBox textBox;
        private bool textBoxWatermarkTextKeyPressed = false;
        public GroupBoxTextBox()
        {
            ClientSize = new Size(303, 50);

            label = new Label()
            {
                AutoSize = false,
                Size = new Size(32, 32),
                Location = new Point(2, 15),
                Name = "label"
            };
            Controls.Add(label);

            textBox = new TextBox()
            {
                Name = "textBox",
                Font = new Font("Calibri", 15, FontStyle.Regular),
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
                textBox.PasswordChar = '\0';
            }
        }
        internal void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox.ForeColor = SystemColors.WindowText;
            if (textBox.Text == watermarkText && !textBoxWatermarkTextKeyPressed)
            {
                textBox.Text = "";
                textBoxWatermarkTextKeyPressed = true;
                textBox.PasswordChar = passwordChar;
            }
            if (e.KeyChar.ToString() == "\b" && textBox.Text.Length == 1)
                TextBoxWatermarked();
        }
        /// <summary>
        /// Проверяет заполнена ли строка
        /// </summary>
        internal bool TextBoxComplete { get { return Checks.TextBoxComplete(textBox, watermarkText); } }

        /// <summary>
        /// Записывает водный знак в TextBox
        /// </summary>
        private void TextBoxWatermarked()
        {
            textBox.ForeColor = SystemColors.GrayText;
            textBox.Text = watermarkText;
            textBoxWatermarkTextKeyPressed = false;
            textBox.PasswordChar = '\0';
        }
    }
}
