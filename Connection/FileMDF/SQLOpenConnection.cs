using DataBase.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase.Connection.FileMDF
{
    public class SQLOpenConnection : IDataProvider
    {
        public bool DataBaseConnection { get; }
        public string DataBaseConnectionString { get; }

        private SqlConnection sqlConnection;
        /// <summary>
        /// Создает объект соединения с Базой Данных.
        /// После создания объекта доступны 2 переменных.
        /// Connection и ConnectionString.
        /// </summary>
        public SQLOpenConnection(string connectionString)
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                try { sqlConnection.Open(); DataBaseConnection = true; DataBaseConnectionString = connectionString; }
                catch { DataBaseConnection = false; }
            }
            catch { DataBaseConnection = false; }
        }

        public List<string> GetTablesNamesFromDataBase()
        => (from DataRow row in sqlConnection.GetSchema("Tables").Rows.Cast<DataRow>()
            select row["TABLE_NAME"].ToString()).ToList();

        public bool TableCheck(string tableName)
        {
            SqlCommand sqlCommand = new SqlCommand($"SELECT Id FROM {tableName}", sqlConnection);
            try
            {
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Close();
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
            SqlCommand mySqlCommand = new SqlCommand(requestString, sqlConnection);
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
            SqlCommand sqlCommand = new SqlCommand($"select * from {tableName}", sqlConnection);

            return ReadFromTable(sqlCommand);
        }

        private DataTable ReadFromTable(SqlCommand sqlCommand)
        {
            SqlDataReader dr = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }

        
    }
}
