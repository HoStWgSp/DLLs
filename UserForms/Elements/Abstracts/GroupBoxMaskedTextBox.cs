using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UserData.UserForms.Elements.Abstracts
{
    internal abstract class GroupBoxMaskedTextBox : GroupBox
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
        /// Устанавливает маску в MaskedTextBox
        /// </summary>
        internal string MaskedTextBoxMaks { set { maskedTextBox.Mask = value; } }



        private Label label;
        internal MaskedTextBox maskedTextBox;

        public GroupBoxMaskedTextBox()
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

            maskedTextBox = new MaskedTextBox()
            {
                Name = "maskedTextBox",
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
