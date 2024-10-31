using DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase.Connection.FileMDF
{
    public class OpenConnection : IDataProvider
    {
        /// <summary>
        /// True - соединение открыто. False - Закрыто.
        /// </summary>
        bool connection { get; set; } = false;
        bool IDataProvider.Connection { get { return connection; } set { } }

        /// <summary>
        /// Объект подключения к Базе Данных.
        /// </summary>
        public SqlConnection SqlConnection {  get; private set; }
        /// <summary>
        /// Создает объект соединения с Базой Данных.
        /// После создания объекта доступны 2 переменных.
        /// Connection и ConnectionString.
        /// </summary>
        public OpenConnection(DataBase dataBase)
        {
            SqlConnection = new SqlConnection(dataBase.ConStr);
            try { SqlConnection.Open(); connection = true; }
            catch 
            {
                
            }
        }

        public bool TableCheck(string tableName)
        {
            throw new NotImplementedException();
        }

        public bool RequestToDB(string requestString)
        {
            throw new NotImplementedException();
        }
    }
}
