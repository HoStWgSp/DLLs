using System.Drawing;
using System.Windows.Forms;

namespace UserData.MyElements
{
    internal class UserGroupBoxes : MyGroupBox
    {
        public UserGroupBoxes(string groupBoxText, string groupBoxName, int X, int Y, Image labelImage,
            string textBoxWatermarkText, int textBoxMaxLength, string textBox2WatermarkText = "")
            : 
            base(groupBoxText, groupBoxName, X, Y, labelImage)
        {
            watermarkText = textBoxWatermarkText;
            watermark2Text = textBox2WatermarkText;

            TextBoxCreate(textBoxWatermarkText, textBoxMaxLength);

            if (textBox2WatermarkText != "")
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
        public UserGroupBoxes(string groupBoxText, string groupBoxName, int X, int Y, Image labelImage):
            base(groupBoxText, groupBoxName, X, Y, labelImage)
        {
            MaskedTextBoxCreate();
        }
        public UserGroupBoxes(string groupBoxText, string groupBoxName, int X, int Y, Image labelImage, string[] groups):
            base(groupBoxText, groupBoxName, X, Y, labelImage)
        {
            ComboBoxCreate(groups);
        }
    }
}
