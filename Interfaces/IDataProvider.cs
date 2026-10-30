using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    internal interface IDataProvider : IDataProviderConnection
    {

        /// <summary>
        /// Смотрит пустая ли таблица
        /// </summary>
        /// <returns></returns>
        //bool TableEmpty(string tableName);

        /// <summary>
        /// Создает список имен всех таблиц в базе данных.
        /// </summary>
        /// <returns></returns>
        //List<string> GetTablesNamesFromDataBase();

        /// <summary>
        /// Получает таблицу из базы данных по имени и возвращает ее в DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //DataTable GetFullTableFromDataBase(string tableName);

        /// <summary>
        /// Читает таблицу из БД и записывает в DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //DataTable GetDataFromTable(string requestString);

        /// <summary>
        /// Возвращает количество строк в таблице
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //int BaseTableRowCount(string tableName);

        /// <summary>
        /// Возвращает данные столбца строки по номеру строки
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowid"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        //string GetStringById(string tableName, int rowid, string columnName);

        /// <summary>
        /// Проверяет наличие таблицы
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool TableCheck(string tableName);

        /// <summary>
        /// Создает таблицу
        /// </summary>
        bool NewTableCreation(string creationString);

        /// <summary>
        /// Добавляет строку в таблицу
        /// </summary>
        /// <param name="requestS
        bool AddTableRow(string requestString);

        /// <summary>
        /// Возвращает номер строки которая первая совпадает с поиском
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        int FindRowIdInTable(string requestString);
    }
}
