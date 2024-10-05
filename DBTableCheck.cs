using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WorkWithDB
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
    }
}
