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
                $"[{Vars.UserPhone}] NVARCHAR({Vars.UserPhoneL}) NULL)");
        }

        /// <summary>
        /// Добавляет пользователя в таблицу пользователей.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool Add(SqlConnection sqlConnection, string userName, string userLastName,
            string userPassword, string userEMail, string userPhone)
        {
            return WorkWithDB.RequestToSQL.RequestExecuteNonQuery(sqlConnection,
                $"INSERT INTO {Vars.TableName} (" +
                $"{Vars.UserName}, " +
                $"{Vars.UserLastName}, " +
                $"{Vars.UserPassword}, " +
                $"{Vars.UserEMail}, " +
                $"{Vars.UserPhone}" +
                $") VALUES (" +
                $"'{userName}', " +
                $"'{userLastName}', " +
                $"'{userPassword}', " +
                $"'{userEMail}', " +
                $"'{userPhone}')"
                );
        }
    }
}
