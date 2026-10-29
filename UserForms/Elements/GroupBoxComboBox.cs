using System.Drawing;
using System.Windows.Forms;

namespace UserData.UserForms.Elements
{
    internal class GroupBoxComboBox : GroupBox
    {
        private Label label;
        private ComboBox comboBox;
        public GroupBoxComboBox(int X, int Y, string[] groups = null)
        {

            ClientSize = new Size(303, 50);
            Text = Vars.Group;
            Location = new Point(X, Y);
            Name = Vars.Group;

            label = new Label()
            {
                AutoSize = false,
                Size = new Size(32, 32),
                Location = new Point(2, 15),
                Image = Properties.Resources.Group,
                Name = "label"
            };
            Controls.Add(label);

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
        /// <summary>
        /// Проверяет заполнен ли ComboBox
        /// </summary>
        internal bool GroupComboBoxSelected 
        {
            get
            {
                if (comboBox.Text != "" &&
                    comboBox.Text != null) return true;

                return false;
            }
        }

        internal string ComboBoxText { get { return comboBox.Text; } set { comboBox.Text = value; } }
        internal bool ComboBoxEnable { set {  comboBox.Enabled = value; } }
    }
}
