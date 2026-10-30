using System.Windows.Forms;
using System.Drawing;
using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using DataBase.Interfaces;
using System.Data;
using System.Collections.Generic;

namespace DataBase
{
    /// <summary>
    /// Подключается к БД.
    /// </summary>
    public class DB : DBConnection
    {
        public DB(string connectionString = "", Icon icon = null) :base(connectionString, icon) { }
        
        //-------------------------------------------------------------

        /// <summary>
        /// Если true то подсоединение к базе данных успешно
        /// </summary>
        public bool DataBaseConnected { get { try { return DataProvider.DataBaseConnection; } catch { return false; } } }

        /// <summary>
        /// Строка для подключения к базе данных
        /// </summary>
        public string DataBaseConnectionString { get { return DataProvider.DataBaseConnectionString; } }

        /// <summary>
        /// Создает список имен всех таблиц в базе данных по алфавиту
        /// </summary>
        //public List<string> TablesNames { get { List<string> list = DataProvider.GetTablesNamesFromDataBase(); list.Sort(); return list; } }

        /// <summary>
        /// Получает таблицу из базы данных и возвращает ее в DataTable
        /// </summary>
        //public DataTable GetFullTableFromDataBase(string tableName) { return DataProvider.GetFullTableFromDataBase(tableName); }
        
        /// <summary>
        /// Проверяет наличие таблицы
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool TableCheck(string tableName) { return DataProvider.TableCheck(tableName); }

        /// <summary>
        /// Создает новую таблицу
        /// </summary>
        /// <param name="creationString"></param>
        /// <returns></returns>
        public bool NewTableCreation(string creationString) { return DataProvider.NewTableCreation(creationString); }

        /// <summary>
        /// Добавляет новую строку в таблицу
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        public bool AddTableRow(string requestString) { return DataProvider.AddTableRow(requestString); }
                
        //public DataTable GetDataFromTable(string requestString) { return DataProvider.GetDataFromTable(requestString); }

        /// <summary>
        /// Читает таблицу из БД и записывает в DataTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //public DataTable GetDataTable(string tableName) { return DataProvider.GetDataFromTable($"select * from {tableName}"); }

        /// <summary>
        /// Заменяет данные в строке
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        //public bool ChangeTableRow(string requestString) { return DataProvider.ExecuteNonQuery(requestString); }

        /// <summary>
        /// Удаляет таблицу из базы данных
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        //public bool DropDataBaseTable(string tableName) { return DataProvider.ExecuteNonQuery($"DROP TABLE {tableName}"); }

        /// <summary>
        /// Считает сколько строк в таблице
        /// </summary>
        //public int BaseTableRowCount(string tableName) { return DataProvider.BaseTableRowCount(tableName); }

        /// <summary>
        /// Возвращает номер строки которая первая совпадает с поиском
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        public int FindRowIdInTable(string requestString) { return DataProvider.FindRowIdInTable(requestString); }

        /// <summary>
        /// Возвращает данные столбца строки по номеру строки
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="rowId"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        //public string GetStringFromTable(string tableName, int rowId, string columnName) { return DataProvider.GetStringById(tableName, rowId, columnName); }
    }
}
