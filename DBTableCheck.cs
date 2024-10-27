using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace DataBase
{
    public class DBTableCheck
    {
        /// <summary>
        /// Проверяет, существует ли таблица в базе данных. Возвращает true, если таблица существует.
        /// </summary>
        public static bool TableCheck(SqlConnection sqlConnection, string tableName)
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
        public static bool TableCheck(MySqlConnection mySqlConnection, string tableName)
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
    }
}
