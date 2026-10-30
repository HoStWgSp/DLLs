using DataBase.Elements;
using DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public class DBConnection
    {
        // Список доступных версий Баз данных
        internal string[] DBList = new string[] { Vars.mdfFile, Vars.MySQL };

        private Icon icon;

        //internal DBStartForm dBStartForm;

        // Создаем объект подключения к БД
        internal IDataProvider DataProvider { get; set; }

        // Подключаемся к базе данных
        public DBConnection(string connectionString = "", Icon icon = null)
        {
            this.icon = icon;
            // Если строка подключения отсутствует
            if (connectionString == "" || connectionString == null)
            // Запускаем форму выбора файла БД или подключения к БД
            //DBStartForm();
            {
                DBStartForm dBStartForm = new DBStartForm(this, DBList, icon); 
            }

            // Если строка есть
            else
                // Проверяем подключение
                ConnectionsCheck(connectionString);
        }

        //-------------------------------------------------------------

        /// <summary>
        /// Проверяет подключения к БД. Если нет подключения ни к одной БД, выводит сообщение
        /// </summary>
        private void ConnectionsCheck(string connectionString)
        {
            DataProvider = new Connection.FileMDF.SQL(connectionString);
            if (DataProvider.DataBaseConnection) return;

            DataProvider = new Connection.MySQL.MySQLOpenConnection(connectionString);
            if (DataProvider.DataBaseConnection) return;

            MessageBox.Show(Vars.DBConnectionFailed);
            DBStartForm dBStartForm = new DBStartForm(this, DBList, icon);
        }
    }
}
