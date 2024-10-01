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
        /// Проверяет существование пользователя. И возвращает строку с данными найденного пользователя.
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
    }
}
