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
    public class WorkWithUser
    {
        /// <summary>
        /// True - Пользователь существует. False - не существует.
        /// </summary>
        public static bool UserExsist { get; private set; } = false;
        /// <summary>
        /// True - Таблица с пользователями существует. False - не существует.
        /// </summary>
        public static bool TableExsist { get; private set; } = false;
        
        /// <summary>
        /// Проверяет существование пользователя. 
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void UserAauthorization(SqlConnection sqlConnection, string username, string password)
        {
            if (!WorkWithDB.DBTableCheck.TableCheck(sqlConnection, Vars.TableName))
            {
                AlertConfirm.AlertConfirm alertConfirm = new AlertConfirm.AlertConfirm();
                if (!alertConfirm.Confirmation(Vars.TableNotExsist, 300, 90)) return;

                // Создание таблицы с пользователями
                TableExsist = TableActions.Creation(sqlConnection);
                if (!TableExsist) {MessageBox.Show(Vars.TableNotCreated); return; }
            }
            // Проверка существования пользователя
            DataTable usersDataTable = WorkWithDB.RequestToSQL.ExecuteReaderToDataTable(sqlConnection,
                $"select * from {Vars.TableName} where {Vars.UserName}='{username}' and {Vars.UserPassword}='{password}'");
            if (usersDataTable.Rows.Count == 0) return;
            Продолжить извлекать данные о пользователе из табоицы


            string dsaf = "";
        }
    }
}
