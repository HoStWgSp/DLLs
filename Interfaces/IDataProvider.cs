using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces
{
    public interface IDataProvider
    {/// <summary>
     /// true - открыто соединение.
     /// </summary>
        bool ConnectionState { get; }


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
        DataTable ReadDataTable(string tableName);

        /// <summary>
        /// Заменяет данные в строке
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        bool ChangeTableRow(string requestString);
    }
}
