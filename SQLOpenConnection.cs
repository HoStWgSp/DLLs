
using DataProvider.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    internal class SQLOpenConnection : IDataProvider
    {
        /// <summary>
        /// True - соединение открыто. False - Закрыто.
        /// </summary>
        bool connection { get; set; } = false;
        bool IDataProvider.ConnectionState { get { return connection; } }

        /// <summary>
        /// Объект подключения к Базе Данных.
        /// </summary>
        private SqlConnection sqlConnection;
        /// <summary>
        /// Создает объект соединения с Базой Данных.
        /// После создания объекта доступны 2 переменных.
        /// Connection и ConnectionString.
        /// </summary>
        public SQLOpenConnection(string dataSource, string attachDBFilename, bool integratedSecurity = true, bool trustServerCertificate = true)
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = dataSource,
                AttachDBFilename = attachDBFilename,
                IntegratedSecurity = integratedSecurity,
                TrustServerCertificate = trustServerCertificate
            };

            sqlConnection = new SqlConnection(stringBuilder.ConnectionString);
            try { sqlConnection.Open(); connection = true; }
            catch 
            {
                
            }
        }

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
