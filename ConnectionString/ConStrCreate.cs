using System.Windows.Forms;
using System.Drawing;
using System;

namespace WorkWithDB.ConnectionString
{
    /// <summary>
    /// ConStrCreate - класс для создания строки подключения. Хранит в ConStr
    /// </summary>
    public class ConStrCreate : Form
    {
        private MainPanel mainPanel = new MainPanel();
        private ComboBox DBcomboBox;

        private string[] DBList = new string[] { Vars.mdfFile, Vars.SQLDB };

        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string ConStr { get; set; }
        
        public ConStrCreate() 
        {
            Size = new System.Drawing.Size(300, 200);
            MinimizeBox = false;
            MaximizeBox = false;

            Label DBLabel = new Label()
            {
                Text = Vars.DBLabelText,
                Location = new Point(2, 3),
                AutoSize = true,
                Font = Fonts.SearchLabelFont()
            };

            DBcomboBox = new ComboBox()
            {
                Location = new Point(DBLabel.Width + DBLabel.Location.X + 2, 3),
                Text = Vars.mdfFile
            };
            DBcomboBox.Items.AddRange(DBList);
            DBcomboBox.SelectedIndexChanged += DBcomboBox_SelectedIndexChanged;

            mainPanel = new MDFPanel(this) { Location = new Point(0, 30) };

            Controls.Add(DBLabel);
            Controls.Add(DBcomboBox);
            Controls.Add(mainPanel);

            ShowDialog();
        }
        private void DBcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controls.Remove(mainPanel);
            if (DBcomboBox.Text == Vars.mdfFile) { mainPanel = new MDFPanel(this);}
            if (DBcomboBox.Text == Vars.SQLDB) { mainPanel = new SQLPanel(); }
            mainPanel.Location = new Point(0, 30);
            string fdsfa = mainPanel.ConStr;
            Controls.Add(mainPanel);
        }
    }
}
