using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserData.UserForms.Elements
{
    internal class GroupBoxMaskedTextBox : GroupBox
    {
        private Label label;
        public MaskedTextBox maskedTextBox;

        public GroupBoxMaskedTextBox(string groupBoxText, Image image, int X, int Y, string mask)
        {
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
        private void MaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            maskedTextBox.ForeColor = SystemColors.WindowText;
        }
    }
}
