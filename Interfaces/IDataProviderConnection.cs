using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    internal interface IDataProviderConnection
    {
        /// <summary>
        /// true - открыто соединение.
        /// </summary>
        bool DataBaseConnection { get; }

        /// <summary>
        /// Содержит строку подключения к базе данных
        /// </summary>
        string DataBaseConnectionString { get; }
    }
}
