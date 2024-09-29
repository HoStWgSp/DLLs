using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WorkWithDB
{
    /// <summary>
    /// Проверяет, существует ли таблица в базе данных. Возвращает true, если таблица существует.
    /// </summary>
    public class DBTableCheck
    {
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
