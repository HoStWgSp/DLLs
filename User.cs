using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WorkWithUser
{
    public class User
    {
        SqlConnection sqlConnection;

        public User(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
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
        public bool Aauthorization(string username, string password)
        {
            UserAauthorization userAauthorization = new UserAauthorization(sqlConnection, username, password);
            if (userAauthorization.UserExsist)
            {
                UserName = userAauthorization.UserName;
                UserLastName = userAauthorization.UserLastName;
                UserEMail = userAauthorization.UserEMail;
                UserGroup = userAauthorization.UserGroup;

                return true;
            }
            return false;
        }

      
    }
}
