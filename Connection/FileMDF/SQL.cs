using DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataBase.Connection.FileMDF
{
    internal class SQL : SQLOpenConnection, IDataProvider
    {

        internal SQL(string connectionString):base(connectionString) { }
        
        //public List<string> GetTablesNamesFromDataBase()
        //=> (from DataRow row in sqlConnection.GetSchema("Tables").Rows.Cast<DataRow>()
        //    select row["TABLE_NAME"].ToString()).ToList();
        //public DataTable GetFullTableFromDataBase(string tableName)
        //{
        //    SqlCommand sqlCommand = new SqlCommand($"select * from {tableName}", sqlConnection);
        //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        //    DataTable dataTable = new DataTable();
        //    sqlDataAdapter.Fill(dataTable);

        //    return dataTable;
        //}
        //public int BaseTableRowCount(string tableName)
        //{
        //    SqlCommand SqlCommand = new SqlCommand($"select count(*) from {tableName}", sqlConnection);
        //    try { return (int)SqlCommand.ExecuteScalar(); }
        //    catch { return 0; }
        //}
        public int FindRowIdInTable(string requestString)
        {
            int rowId = 0;
            SqlCommand sqlCommand = new SqlCommand(requestString, sqlConnection);

            try { rowId = Convert.ToInt32(sqlCommand.ExecuteScalar()); }
            catch { }

            return rowId;
        }
        //public string GetStringById(string tableName, int rowid, string columnName)
        //{
        //    SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM {tableName} WHERE Id='{rowid}'", sqlConnection);
        //    SqlDataReader reader = sqlCommand.ExecuteReader();

        //    if (reader.HasRows)
        //    {
        //        return reader[columnName].ToString();
        //    }
        //    return "";
        //}
        //public DataTable GetDataFromTable(string requestString)
        //{
        //    SqlCommand sqlCommand = new SqlCommand(requestString, sqlConnection);

        //    return ReadFromTable(sqlCommand);
        //}
        //private DataTable ReadFromTable(SqlCommand sqlCommand)
        //{
        //    SqlDataReader dr = sqlCommand.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    return dt;
        //}
        //public bool TableEmpty(string tableName) { return false; }
        public bool TableCheck(string tableName) { return ExecuteReader($"SELECT * FROM {tableName}"); }
        public bool NewTableCreation(string creationString) { return ExecuteNonQuery(creationString); }
        public bool AddTableRow(string requestString) { return ExecuteNonQuery(requestString); }
    }
}
