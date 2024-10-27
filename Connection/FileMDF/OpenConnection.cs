using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase.Connection.FileMDF
{
    public class OpenConnection
    {
        /// <summary>
        /// True - соединение открыто. False - Закрыто.
        /// </summary>
        public bool Connection {  get; private set; }

        /// <summary>
        /// Хранит строку подключения к Базе Данных.
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Объект подключения к Базе Данных.
        /// </summary>
        public SqlConnection SqlConnection {  get; private set; }

        /// <summary>
        /// Создает объект соединения с Базой Данных.
        /// После создания объекта доступны 2 переменных.
        /// Connection и ConnectionString.
        /// </summary>
        public OpenConnection(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
            try { SqlConnection.Open(); ConnectionString = connectionString; Connection = true; }
            catch 
            { 
                //DataBase conStrCreate = new DataBase(); 
                //if (conStrCreate.ConStr != "")
                //{
                //    Connection = true;
                //    ConnectionString = conStrCreate.ConStr;
                //    SqlConnection = conStrCreate.SqlConnection;
                //}
                //else
                //{
                //    Connection = false;
                //}
            }
        }
    }
}
