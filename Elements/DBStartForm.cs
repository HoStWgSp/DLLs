using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase.Elements
{
    public class DBStartForm : Form
    {
        private DBConnection dBConnection;

        // Создаем объект панели
        internal Connection.MainPanel mainPanel;

        // Создаем ComboBox
        internal ComboBox DBcomboBox;

        /// <summary>
        /// Форма для выбора типа БД
        /// </summary>
        public DBStartForm(DBConnection dBConnection, string[] DBList, Icon icon)
        {
            this.dBConnection = dBConnection;
            // Выставляем иконку для окна
            Icon = icon;

            this.dBConnection = dBConnection;
            Size = new Size(300, 200);
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

            // Загружает панель для выбора файла .mdf
            mainPanel = new Connection.FileMDF.MDFPanel(dBConnection, this) { Location = new Point(0, 30) };

            Controls.Add(DBLabel);
            Controls.Add(DBcomboBox);
            Controls.Add(mainPanel);

            ShowDialog();
        }
        private void DBcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controls.Remove(mainPanel);
            if (DBcomboBox.Text == Vars.mdfFile) { mainPanel = new Connection.FileMDF.MDFPanel(dBConnection, this); }
            if (DBcomboBox.Text == Vars.MySQL) { mainPanel = new Connection.MySQL.MySQLPanel(dBConnection, this); }
            mainPanel.Location = new Point(0, 30);
            Controls.Add(mainPanel);
        }
    }
}
