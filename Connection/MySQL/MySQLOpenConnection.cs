using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBase.Connection;
using MySql.Data.MySqlClient;

namespace DataBase.Connection.MySQL

{
    public class MySQLOpenConnection
    {
        public MySqlConnection MySqlConnection {  get; private set; }

        /// <summary>
        /// Создает объект соединения с Базой Данных.
        /// После создания объекта доступны 2 переменных.
        /// Connection и ConnectionString.
        /// </summary>
        public MySQLOpenConnection(DataBase conStrCreate)
        {
            MySqlConnection = new MySqlConnection(conStrCreate.ConStr);
            try { MySqlConnection.Open(); conStrCreate.MySqlConnection = MySqlConnection; }
            catch 
            {
                conStrCreate.ConStr = "";
            }
        }
    }
}
