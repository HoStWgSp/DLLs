using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithUser
{
    public class UsersTableActions
    {
        /// <summary>
        /// Проверяет существование таблицы с данными пользователей.
        /// Возвращает true, если таблица существует.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public static bool UsersTableCheck(SqlConnection sqlConnection)
        {
            return WorkWithDB.DBTableCheck.TableCheck(sqlConnection, Vars.TableName);
        }
        
        /// <summary>
        /// Создает таблицу Users
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool Creation(SqlConnection sqlConnection)
        {
            return WorkWithDB.RequestToSQL.RequestExecuteNonQuery(sqlConnection,
                $"CREATE TABLE [dbo].[" + Vars.TableName + "](" +
                $"[Id] INT IDENTITY (1, 1) NOT NULL," +
                $"[{Vars.UserName}] NVARCHAR({Vars.UserNamesL}) NULL," +
                $"[{Vars.UserLastName}] NVARCHAR({Vars.UserNamesL}) NULL," +
                $"[{Vars.UserPassword}] NVARCHAR({Vars.UserPasswordL}) NULL," +
                $"[{Vars.UserEMail}] NVARCHAR({Vars.UserEMailL}) NULL," +
                $"[{Vars.UserGroup}] NVARCHAR({Vars.UserGroupL}) NULL)");
        }
    }
}
