using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBase.Connection;
using DataBase.Interfaces;
using MySql.Data.MySqlClient;

namespace DataBase.Connection.MySQL

{
    public class MySQLOpenConnection : IDataProvider
    {
        public MySqlConnection MySqlConnection {  get; private set; }

        bool connection { get; set; } = false;
        bool IDataProvider.Connection { get { return connection; } set { } }

        /// <summary>
        /// Создает объект соединения с Базой Данных.
        /// После создания объекта доступны 2 переменных.
        /// Connection и ConnectionString.
        /// </summary>
        public MySQLOpenConnection(DataBase dataBase)
        {
            MySqlConnection = new MySqlConnection(dataBase.ConStr);
            try { MySqlConnection.Open(); connection = true; }
            catch 
            {
                
            }
        }

        public bool TableCheck(string tableName)
        {
            MySqlCommand mySqlCommand = new MySqlCommand($"SELECT Id FROM {tableName}", MySqlConnection);
            try
            {
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                mySqlDataReader.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RequestToDB(string requestString)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(requestString, MySqlConnection);
            try
            {
                mySqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}
