using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WorkWithUser
{
    internal class UsersTableActions
    {        
        /// <summary>
        /// Создает таблицу Users
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        internal static bool Creation(SqlConnection sqlConnection)
        {
            return RequestExecuteNonQuery(sqlConnection,
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
        internal static bool Add(SqlConnection sqlConnection, Dictionary<string, string> userData)
        {
            return RequestExecuteNonQuery(sqlConnection,
                $"INSERT INTO {Vars.TableName} (" +
                $"{Vars.UserName}, " +
                $"{Vars.UserLastName}, " +
                $"{Vars.UserPassword}, " +
                $"{Vars.UserEMail}, " +
                $"{Vars.UserPhone}, " +
                $"{Vars.UserGroup}" +
                $") VALUES (" +
                $"N'{userData[Vars.UserName]}', " +
                $"N'{userData[Vars.UserLastName]}', " +
                $"N'{userData[Vars.UserPassword]}', " +
                $"N'{userData[Vars.UserEMail]}', " +
                $"N'{userData[Vars.UserPhone]}', " +
                $"N'{userData[Vars.UserGroup]}')"
                );
        }

        /// <summary>
        /// Заменяет данные в строке
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="rowId"></param>
        /// <returns></returns>
        internal static bool Change(SqlConnection sqlConnection, int rowId)
        {

        }

        /// <summary>
        /// Заменяет все данные в строке на null
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="rowId"></param>
        /// <returns></returns>
        internal static bool RemoveTableRow(SqlConnection sqlConnection, int rowId)
        {
            return RequestExecuteNonQuery(sqlConnection,
                $"UPDATE {Vars.TableName} SET " +
                $"{Vars.UserName}=NULL," +
                $"{Vars.UserLastName}=NULL," +
                $"{Vars.UserPassword}=NULL," +
                $"{Vars.UserEMail}=NULL," +
                $"{Vars.UserPhone}=NULL," +
                $"{Vars.UserGroup}=NULL WHERE ID={rowId}");
        }
        
        
        /// <summary>
        /// Отправляет команду в SQL DataBase и выполняет ExecuteNonQuery()
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static bool RequestExecuteNonQuery(SqlConnection sqlConnection, string queryString)
        {
            SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
            try
            {
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}
