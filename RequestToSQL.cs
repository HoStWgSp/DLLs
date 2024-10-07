using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace WorkWithUser
{
    public class RequestToSQL
    {
        public static bool ConnectionOk { get; private set; }



        /// <summary>
        /// Ищет Id строки в таблице с одной колонкой (имя колонки как у таблицы) по данным из этой колонки
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column_to_check"></param>
        /// <param name="item_to_check"></param>
        /// <returns></returns>
        //public static int FindIdInTable(string tableName, string item_to_check)
        //{
        //    string Id = RequestExecuteScalar($"SELECT Id FROM {tableName} WHERE {tableName}=N'{item_to_check}'");
        //    if (Id == null) return 0;
        //    else return Convert.ToInt32(Id);
        //}

        /// <summary>
        /// Ищет Id строки в таблице по данным из ячейки в одной колонке
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column_to_check"></param>
        /// <param name="item_to_check"></param>
        /// <returns></returns>
        //public static int FindIdInTable(string tableName, string column_to_check, string item_to_check)
        //{
        //    string Id =RequestExecuteScalar($"SELECT Id FROM {tableName} WHERE {column_to_check}=N'{item_to_check}'");
        //    if (Id == null) return 0;
        //    else return Convert.ToInt32(Id);
        //}

        /// <summary>
        /// Находит данные в таблице с одной колонкой (имя колонки как у таблицы) по номеру строки
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowId"></param>
        /// <returns></returns>
        //public static string FindDataInTable(string columnName, int rowId)
        //{
        //    return RequestExecuteScalar($"SELECT {columnName} FROM {columnName} WHERE Id=N'{rowId}'");
        //}

        /// <summary>
        /// Добавляет строку в таблицу с одной колонкой (имя колонки как у таблицы)
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="item_to_check"></param>
        //public static void AddDataToTable(string tableName, string item_to_check)
        //{
        //    RequestExecuteNonQuery($"INSERT INTO {tableName} ({tableName}) Values (N'{item_to_check}')");
        //}



        /// <summary>
        /// Меняет строку в таблице с одной колонкой (имя колонки как у таблицы) по номеру строки
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="item_to_check"></param>
        /// <param name="rowId"></param>
        //public static void ChangeDataInTable(string tableName, string item_to_check, int rowId)
        //{
        //    RequestExecuteNonQuery($"UPDATE {tableName} SET {tableName} = N'{item_to_check}' WHERE Id = '{rowId}'");
        //}

        /// <summary>
        /// Меняет данные в одной ячейке в строке по номеру строки
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column_to_check"></param>
        /// <param name="item_to_check"></param>
        /// <param name="rowId"></param>
        //public static void ChangeDataInTable(string tableName, string column_to_check, string item_to_change, int rowId)
        //{
        //    RequestExecuteNonQuery($"UPDATE {tableName} SET {column_to_check} = N'{item_to_change}' WHERE Id = '{rowId}'");
        //}

        /// <summary>
        /// Отправляет команду в SQL DataBase и выполняет ExecuteNonQuery()
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool RequestExecuteNonQuery(SqlConnection sqlConnection, string queryString)
        {
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
            try
            {
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Отправляет команду в SQL DataBase и выполняет ExecuteScalar()
        /// </summary>
        /// <param name="строкаЗапроса"></param>
        /// <returns></returns>
        //public static string RequestExecuteScalar(string строкаЗапроса)
        //{
        //    SqlConnection sqlConnection = new SqlConnection(ConnectionString.ConStr);
        //    sqlConnection.Open();
        //    SqlCommand sqlCommand = new SqlCommand(строкаЗапроса, sqlConnection);
        //    string str = null;
        //    try
        //    {
        //        if (sqlCommand.ExecuteScalar() != null) str = sqlCommand.ExecuteScalar().ToString();
        //        ConnectionOk = true;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString());
        //        ConnectionOk = false;
        //    }
        //    sqlConnection.Close();
        //    return str;
        //}

        /// <summary>
        /// Производит поиск строк в таблице и возвращает DataTable со списком данных строк.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="request"></param>
        /// <param name="connectinString"></param>
        public static DataTable ExecuteReaderToDataTable(SqlConnection sqlConnection, string QueryString)
        {
            SqlCommand sqlCommand = new SqlCommand(QueryString, sqlConnection);

            SqlDataReader dr = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }
    }
}
