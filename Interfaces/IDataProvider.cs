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

        /// <summary>
        /// Проверяет наличие таблицы
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool TableCheck(string tableName);

        /// <summary>
        /// Создает новую таблицу
        /// </summary>
        /// <param name="creationString"></param>
        /// <returns></returns>
        bool NewTableCreation(string creationString);

        /// <summary>
        /// Добавляет новую строку в таблицу
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        bool AddTableRow(string requestString);
        
        /// <summary>
        /// Читает таблицу из БД и записывает в DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataTable GetDataTable(string tableName);

        /// <summary>
        /// Заменяет данные в строке
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        bool ChangeTableRow(string requestString);

        /// <summary>
        /// Удаляет таблицу из базы данных
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool DropDataBaseTable(string tableName);

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
