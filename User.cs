using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WorkWithUser
{
    public class User
    {
        SqlConnection sqlConnection;
        DataTable usersData;
        DataRow authorizedUser;

        public User(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;

            usersData = WorkWithDB.RequestToSQL.ExecuteReaderToDataTable(sqlConnection,
                $"select * from {Vars.TableName}");
        }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string UserLastName { get; private set; }
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string UserEMail { get; private set; }
        /// <summary>
        /// Рабочая группа пользователя.
        /// </summary>
        public string UserGroup { get; private set; }

        /// <summary>
        /// Проверяет существование пользователя и возвращает его данные.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public bool Aauthorization(string userName, string UserPassword)
        {
            authorizedUser = UserActions.Aauthorization(usersData, userName, UserPassword);
            if (!authorizedUser.IsNull(1))
            {
                UserName = authorizedUser[Vars.UserName].ToString();
                UserLastName = authorizedUser[Vars.UserLastName].ToString();
                UserEMail = authorizedUser[Vars.UserEMail].ToString();
                UserGroup = authorizedUser[Vars.UserGroup].ToString();
                return true;
            }
            return false;
        }
    }
}
