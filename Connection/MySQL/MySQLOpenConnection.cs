using System;
using System.Collections.Generic;
using System.Data;
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
        MySqlConnection mySqlConnection;

        bool connection;
        bool IDataProvider.Connection { get { return connection; } set { } }
        string IDataProvider.DataBase { get { return mySqlConnection.Database; } }

        /// <summary>
        /// Создает объект соединения с Базой Данных.
        /// После создания объекта доступны 2 переменных.
        /// Connection и ConnectionString.
        /// </summary>
        public MySQLOpenConnection(string connectionString)
        {
            mySqlConnection = new MySqlConnection(connectionString);
            try { mySqlConnection.Open(); connection = true; }
            catch { connection = false; }
        }

        public bool TableCheck(string tableName)
        {
            MySqlCommand mySqlCommand = new MySqlCommand($"SELECT Id FROM {tableName}", mySqlConnection);
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

        public bool NewTableCreation(string creationString) { return ExecuteNonQueryAction(creationString); }
        public bool AddTableRow(string requestString) { return ExecuteNonQueryAction(requestString); }
        public bool ChangeTableRow(string requestString) { return ExecuteNonQueryAction(requestString); }
        private bool ExecuteNonQueryAction(string requestString)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(requestString, mySqlConnection);
            try
            {
                mySqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }





        public DataTable ReadDataTable(string tableName)
        {
            MySqlCommand mySqlCommand = new MySqlCommand($"select * from {tableName}", mySqlConnection);
            return ReadFromTable(mySqlCommand);
        }
        private DataTable ReadFromTable(MySqlCommand mySqlCommand)
        {
            MySqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }

        
    }
}
