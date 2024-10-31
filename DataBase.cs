using System.Windows.Forms;
using System.Drawing;
using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using DataBase.Interfaces;

namespace DataBase
{
    /// <summary>
    /// ConStrCreate - класс для создания строки подключения. Созданную строку хранит в ConStr
    /// </summary>
    public class DataBase : Form
    {
        private Connection.MainPanel mainPanel;// = new Connection.MainPanel();
        private ComboBox DBcomboBox;

        private string[] DBList = new string[] { Vars.mdfFile, Vars.MySQL };

        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string ConStr { get; set; } = "";
        public SqlConnection SqlConnection {  get; set; }
        public MySqlConnection MySqlConnection { get; set; }
        
        public IDataProvider dataProvider { get; set; }

        public DataBase(Icon icon = null)
        {
            
            Icon = icon;
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

            mainPanel = new Connection.FileMDF.MDFPanel(this) { Location = new Point(0, 30) };

            Controls.Add(DBLabel);
            Controls.Add(DBcomboBox);
            Controls.Add(mainPanel);

            ShowDialog();
        }

        public DataBase(string connectionString, Icon icon = null)
        {
            Icon = icon;
            ConStr = connectionString;
            dataProvider = new Connection.FileMDF.OpenConnection(this);
            if (dataProvider.Connection) return;

            dataProvider = new Connection.MySQL.MySQLOpenConnection(this);
            if (dataProvider.Connection) return;
        }

        private void DBcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controls.Remove(mainPanel);
            if (DBcomboBox.Text == Vars.mdfFile) { mainPanel = new Connection.FileMDF.MDFPanel(this);}
            if (DBcomboBox.Text == Vars.MySQL) { mainPanel = new Connection.MySQL.MySQLPanel(this); }
            mainPanel.Location = new Point(0, 30);
            string fdsfa = mainPanel.ConStr;
            Controls.Add(mainPanel);
        }


        /// <summary>
        /// Проверяет наличие таблицы с определенным именем.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool TableCheck(string tableName)
        {
            return dataProvider.TableCheck(tableName);
        }
        public bool RequestExecuteNonQuery(string requestString)
        {
            return dataProvider.RequestToDB(requestString);
        }

        
    }
}
