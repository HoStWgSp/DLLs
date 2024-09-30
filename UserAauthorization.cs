using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWithUser
{
    internal class UserAauthorization
    {
        /// <summary>
        /// True - Пользователь существует. False - не существует.
        /// </summary>
        public bool UserExsist { get; private set; } = false;

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string UserLastName {  get; private set; }
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string UserEMail {  get; private set; }
        /// <summary>
        /// Рабочая группа пользователя.
        /// </summary>
        public string UserGroup { get; private set; }
        
        /// <summary>
        /// Проверяет существование пользователя. 
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public UserAauthorization(SqlConnection sqlConnection, string username, string password)
        {
            // Проверка существования пользователя
            DataTable usersDataTable = WorkWithDB.RequestToSQL.ExecuteReaderToDataTable(sqlConnection,
                $"select * from {Vars.TableName} where {Vars.UserName}='{username}' and {Vars.UserPassword}='{password}'");

            if (usersDataTable.Rows.Count > 0)
            {
                UserExsist = true;
                UserName = usersDataTable.Rows[0][Vars.UserName].ToString();
                UserLastName = usersDataTable.Rows[0][Vars.UserLastName].ToString();
                UserEMail = usersDataTable.Rows[0][Vars.UserEMail].ToString();
                UserGroup = usersDataTable.Rows[0][Vars.UserGroup].ToString();
                return;
            }
            UserExsist = false;
        }
    }
}
