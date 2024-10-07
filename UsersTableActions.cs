using System.Collections.Generic;
using System.Data.SqlClient;

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
                $"[{Vars.UserName}] NVARCHAR({Vars.UserTextL}) NULL," +
                $"[{Vars.UserLastName}] NVARCHAR({Vars.UserTextL}) NULL," +
                $"[{Vars.UserPassword}] NVARCHAR({Vars.UserTextL}) NULL," +
                $"[{Vars.UserEMail}] NVARCHAR({Vars.UserEMailL}) NULL," +
                $"[{Vars.UserPhone}] NVARCHAR({Vars.UserPhoneL}) NULL," +
                $"[{Vars.UserGroup}] NVARCHAR({Vars.UserTextL}) NULL)");
        }

        /// <summary>
        /// Добавляет пользователя в таблицу пользователей.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static bool Add(SqlConnection sqlConnection, Dictionary<string, string> userData)
        {
            return WorkWithDB.RequestToSQL.RequestExecuteNonQuery(sqlConnection,
                $"INSERT INTO {Vars.TableName} (" +
                $"{Vars.UserName}, " +
                $"{Vars.UserLastName}, " +
                $"{Vars.UserPassword}, " +
                $"{Vars.UserEMail}, " +
                $"{Vars.UserPhone}, " +
                $"{Vars.UserGroup}" +
                $") VALUES (" +
                $"'{userData[Vars.UserName]}', " +
                $"'{userData[Vars.UserLastName]}', " +
                $"'{userData[Vars.UserPassword]}', " +
                $"'{userData[Vars.UserEMail]}', " +
                $"'{userData[Vars.UserPhone]}', " +
                $"'{userData[Vars.UserGroup]}')"
                );
        }
    }
}
