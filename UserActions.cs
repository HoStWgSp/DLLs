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
        /// Проверяет существует ли пользователь с таким именем.
        /// </summary>
        /// <param name="usersDataTable"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool CheckUserName(DataTable usersDataTable, string userName)
        {
            foreach (DataRow dataRow in usersDataTable.Rows)
            {
                if (dataRow[Vars.UserName].ToString() == userName)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Проверяет существование пользователя в DataTable. И возвращает строку с данными найденного пользователя.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static DataRow Aauthorization(DataTable usersDataTable, string userName, string password)
        {
            DataRow authirizedUser = usersDataTable.NewRow();

            // Проверка существования пользователя            
            foreach (DataRow row in usersDataTable.Rows)
            {
                if (row[Vars.UserName].ToString() == userName && row[Vars.UserPassword].ToString() == password)
                    return row;
            }
            return authirizedUser;
        }
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
