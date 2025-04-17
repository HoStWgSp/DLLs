using DataBase.Interfaces;
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

        public DataTable GetFullTableFromDataBase(string tableName)
        {
            SqlCommand sqlCommand = new SqlCommand($"select * from {tableName}", sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        public bool ExecuteReader(string tableName)
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

        public bool ExecuteNonQuery(string requestString) 
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

        public int BaseTableRowCount(string tableName)
        {
            SqlCommand SqlCommand = new SqlCommand($"select count(*) from {tableName}", sqlConnection);
            try { return (int)SqlCommand.ExecuteScalar(); }
            catch { return 0; }
        }
        public int FindRowIdInTable(string requestString)
        {
            int rowId = 0;
            SqlCommand sqlCommand = new SqlCommand(requestString, sqlConnection);

            try { rowId = Convert.ToInt32(sqlCommand.ExecuteScalar()); }
            catch { }

            return rowId;
        }
        public string GetStringById(string tableName, int rowid, string columnName)
        {
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM {tableName} WHERE Id='{rowid}'", sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                return reader[columnName].ToString();
            }
            return "";
        }







        public DataTable GetDataTable(string tableName)
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
