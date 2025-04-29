using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    public interface IDataProvider
    {
        /// <summary>
        /// true - открыто соединение.
        /// </summary>
        bool DataBaseConnection { get; }

        /// <summary>
        /// Содержит строку подключения к базе данных
        /// </summary>
        string DataBaseConnectionString { get; }

        /// <summary>
        /// Создает список имен всех таблиц в базе данных.
        /// </summary>
        /// <returns></returns>
        List<string> GetTablesNamesFromDataBase();

        /// <summary>
        /// Получает таблицу из базы данных по имени и возвращает ее в DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataTable GetFullTableFromDataBase(string tableName);

        bool ExecuteReader(string tableName);

        bool ExecuteNonQuery(string creationString);
        
        /// <summary>
        /// Читает таблицу из БД и записывает в DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataTable GetDataFromTable(string requestString);

        /// <summary>
        /// Возвращает количество строк в таблице
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        int BaseTableRowCount(string tableName);

        /// <summary>
        /// Возвращает номер строки которая первая совпадает с поиском
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        int FindRowIdInTable(string requestString);

        /// <summary>
        /// Возвращает данные столбца строки по номеру строки
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowid"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        string GetStringById(string tableName, int rowid, string columnName);
    }
}
