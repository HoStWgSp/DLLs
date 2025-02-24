using System.Windows.Forms;
using System.Drawing;
using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using DataBase.Interfaces;
using System.Data;
using System.Collections.Generic;

namespace DataBase
{
    /// <summary>
    /// ConStrCreate - класс для создания строки подключения. Созданную строку хранит в ConStr
    /// </summary>
    public class DB : Form
    {



        internal Connection.MainPanel mainPanel;
        internal ComboBox DBcomboBox;
        internal string[] DBList = new string[] { Vars.mdfFile, Vars.MySQL };

        internal IDataProvider DataProvider { get; set; }

        public DB(Icon icon = null, string connectionString = "")
        {
            Icon = icon;
            if (connectionString == "" || connectionString == null)
                DBStartForm();
            else
                ConnectionsCheck(connectionString);
        }
        private void DBStartForm()
        {
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
        private void ConnectionsCheck(string connectionString)
        {
            DataProvider = new Connection.FileMDF.SQLOpenConnection(connectionString);
            if (DataProvider.DataBaseConnection) return;

            DataProvider = new Connection.MySQL.MySQLOpenConnection(connectionString);
            if (DataProvider.DataBaseConnection) return;

            MessageBox.Show("Не удалось подключиться к базе данных. Настройте подключение заново.");
            DBStartForm();
        }
        private void DBcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controls.Remove(mainPanel);
            if (DBcomboBox.Text == Vars.mdfFile) { mainPanel = new Connection.FileMDF.MDFPanel(this);}
            if (DBcomboBox.Text == Vars.MySQL) { mainPanel = new Connection.MySQL.MySQLPanel(this); }
            mainPanel.Location = new Point(0, 30);
            Controls.Add(mainPanel);
        }



        /// <summary>
        /// Если true то подсоединение к базе данных успешно
        /// </summary>
        public bool DataBaseConnected
        {
            get
            {
                try { return DataProvider.DataBaseConnection; }
                catch { return false; }
            }
        }

        /// <summary>
        /// Строка для подключения к базе данных
        /// </summary>
        public string DataBaseConnectionString { get { return DataProvider.DataBaseConnectionString; } }

        /// <summary>
        /// Создает список имен всех таблиц в базе данных по алфавиту
        /// </summary>
        public List<string> TablesNames
        {
            get
            {
                List<string> list = DataProvider.GetTablesNamesFromDataBase();
                list.Sort();
                return list;
            }
        }

        /// <summary>
        /// Получает таблицу из базы данных и возвращает ее в DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetFullTableFromDataBase(string tableName) { return DataProvider.GetFullTableFromDataBase(tableName); }
        

        /// <summary>
        /// Проверяет наличие таблицы
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool TableCheck(string tableName) { return DataProvider.TableCheck(tableName); }

        /// <summary>
        /// Создает новую таблицу
        /// </summary>
        /// <param name="creationString"></param>
        /// <returns></returns>
        public bool NewTableCreation(string creationString) { return DataProvider.NewTableCreation(creationString); }

        /// <summary>
        /// Добавляет новую строку в таблицу
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        public bool AddTableRow(string requestString) { return DataProvider.AddTableRow(requestString); }
                
        /// <summary>
        /// Читает таблицу из БД и записывает в DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable ReadDataTable(string tableName) { return DataProvider.ReadDataTable(tableName); }

        /// <summary>
        /// Заменяет данные в строке
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        public bool ChangeTableRow(string requestString) { return DataProvider.ChangeTableRow(requestString); }

        /// <summary>
        /// Удаляет таблицу из базы данных
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        public bool DropDataBaseTable(string tableName) { return DataProvider.DropDataBaseTable($"DROP TABLE {tableName}"); }
    }
}
