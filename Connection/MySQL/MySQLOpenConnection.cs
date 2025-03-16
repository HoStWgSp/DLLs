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
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DataBase.Connection.MySQL

{
    public class MySQLOpenConnection : IDataProvider
    {
        MySqlConnection mySqlConnection;

        public bool DataBaseConnection { get; }
        public string DataBaseConnectionString { get; }

        /// <summary>
        /// Создает объект соединения с Базой Данных.
        /// После создания объекта доступны 2 переменных.
        /// Connection и ConnectionString.
        /// </summary>
        public MySQLOpenConnection(string connectionString)
        {
            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
                try { mySqlConnection.Open(); DataBaseConnection = true; DataBaseConnectionString = connectionString; }
                catch { DataBaseConnection = false; }
            }
            catch { DataBaseConnection = false; }
        }

        public List<string> GetTablesNamesFromDataBase()
        {
            return null;
        }
        public DataTable GetFullTableFromDataBase(string tableName)
        {
            return new DataTable();
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
        public bool DropDataBaseTable(string requestString) { return ExecuteNonQueryAction(requestString); }
        public int BaseTableRowCount(string tableName)
        {
            MySqlCommand mySqlCommand = new MySqlCommand($"select count(*) from {tableName}", mySqlConnection);
            try { return (int)mySqlCommand.ExecuteScalar(); }
            catch { return 0; }
        }
        public int FindRowIdInTable(string requestString)
        {
            //int rowId;
            //MySqlCommand mySqlCommand = new MySqlCommand(requestString, mySqlConnection);
            //try { rowId = (int)mySqlCommand.ExecuteScalar(); }
            //catch { rowId = 0; }
            //return rowId;
            return 0;
        }
        public string GetStringById(string tableName, int rowid, string columnName)
        {
            return "";
        }


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





        public DataTable GetDataTable(string tableName)
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
