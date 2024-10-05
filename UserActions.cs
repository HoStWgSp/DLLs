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
    internal class UserActions
    {   
        


        /// <summary>
        /// Добавляет пользователя в таблицу пользователей.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool Add(SqlConnection sqlConnection, string[] userData)
        {
            return WorkWithDB.RequestToSQL.RequestExecuteNonQuery(sqlConnection,
                $"INSERT INTO {Vars.TableName} ({Vars.UserName}, {Vars.UserLastName}, {Vars.UserPassword}, {Vars.UserEMail}, {Vars.UserGroup}) " +
                $"VALUES " +
                $"('{userData[0]}', '{userData[1]}', '{userData[2]}', '{userData[3]}', '{userData[4]}')");
        } 

    }
}
