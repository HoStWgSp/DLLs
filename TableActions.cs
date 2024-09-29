using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithUser
{
    internal class TableActions
    {
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
                $"[{Vars.UserName}] NVARCHAR({Vars.UserNames}) NULL," +
                $"[{Vars.UserLastName}] NVARCHAR({Vars.UserNames}) NULL," +
                $"[{Vars.UserNickName}] NVARCHAR({Vars.UserNames}) NULL," +
                $"[{Vars.UserPassword}] NVARCHAR({Vars.UserPasswordL}) NULL," +
                $"[{Vars.UserEMail}] NVARCHAR({Vars.UserEMailL}) NULL," +
                $"[{Vars.UserGroup}] NVARCHAR({Vars.UserGroupL}) NULL)");
        }
    }
}
