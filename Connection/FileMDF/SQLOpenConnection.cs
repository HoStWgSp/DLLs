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
    public class SQLOpenConnection : IDataProviderConnection
    {
        public bool DataBaseConnection { get; }
        public string DataBaseConnectionString { get; }

        internal SqlConnection sqlConnection;

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
        internal bool ExecuteReader(string requestString)
        {
            SqlCommand sqlCommand = new SqlCommand(requestString, sqlConnection);
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
        internal bool ExecuteNonQuery(string requestString) 
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
    }
}
