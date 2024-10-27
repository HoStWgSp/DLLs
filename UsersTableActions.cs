using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UserData
{
    internal class UsersTableActions
    {
        /// <summary>
        /// Проверяет, существует ли таблица в базе данных. Возвращает true, если таблица существует.
        /// </summary>
        internal static bool TableCheck(User user, string tableName)
        {
            SqlCommand sqlCommand = new SqlCommand($"SELECT Id FROM {tableName}", user.sqlConnection);
            try
            {
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Производит поиск строк в таблице и возвращает DataTable со списком данных строк.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="request"></param>
        /// <param name="connectinString"></param>
        public static DataTable ExecuteReaderToDataTable(User user, string QueryString)
        {
            SqlCommand sqlCommand = new SqlCommand(QueryString, user.sqlConnection);

            SqlDataReader dr = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }

        /// <summary>
        /// Создает таблицу Users
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        internal static bool Creation(User user)
        {
            if (!RequestExecuteNonQuery(user.sqlConnection,
                $"CREATE TABLE [dbo].[" + Vars.TableName + "](" +
                $"[Id] INT IDENTITY (1, 1) NOT NULL," +
                $"[{Vars.UserName}] NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"[{Vars.UserLastName}] NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"[{Vars.UserPassword}] NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"[{Vars.UserEMail}] NVARCHAR({Vars.UserEMailL}) NOT NULL," +
                $"[{Vars.UserPhone}] NVARCHAR({Vars.UserPhoneL}) NOT NULL," +
                $"[{Vars.UserGroup}] NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"[{Vars.UserAdmin}] CHAR(1) NOT NULL)")) return false;

            Dictionary<string, string> adminUser = new Dictionary<string, string>()
            {
                { Vars.UserName, "admin" },
                { Vars.UserLastName, "" },
                { Vars.UserEMail, "" },
                { Vars.UserPhone, "" },
                { Vars.UserGroup, "" },
                { Vars.UserPassword, "admin" },
                { Vars.UserAdmin, "1"}
            };

            return Add(user, adminUser);
        }

        /// <summary>
        /// Добавляет пользователя в таблицу пользователей.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        internal static bool Add(User user, Dictionary<string, string> userData)
        {
            foreach (DataRow dataRow in user.UsersData.Rows)
            {
                if ((dataRow[Vars.UserName].ToString() == null  || dataRow[Vars.UserName].ToString() == "") && 
                    (dataRow[Vars.UserLastName].ToString() == null || dataRow[Vars.UserLastName].ToString() == ""))
                {
                    return Change(user, Convert.ToInt32(dataRow["Id"]), userData);
                }
            }
            return RequestExecuteNonQuery(user.sqlConnection,
                $"INSERT INTO {Vars.TableName} (" +
                $"{Vars.UserName}, " +
                $"{Vars.UserLastName}, " +
                $"{Vars.UserPassword}, " +
                $"{Vars.UserEMail}, " +
                $"{Vars.UserPhone}, " +
                $"{Vars.UserGroup}, " +
                $"{Vars.UserAdmin}" +
                $") VALUES (" +
                $"N'{userData[Vars.UserName]}', " +
                $"N'{userData[Vars.UserLastName]}', " +
                $"N'{userData[Vars.UserPassword]}', " +
                $"N'{userData[Vars.UserEMail]}', " +
                $"N'{userData[Vars.UserPhone]}', " +
                $"N'{userData[Vars.UserGroup]}', " +
                $"'{userData[Vars.UserAdmin]}')"
                );
        }

        /// <summary>
        /// Заменяет данные в строке
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="rowId"></param>
        /// <returns></returns>
        internal static bool Change(User user, int rowId, Dictionary<string, string> userData)
        {
            return RequestExecuteNonQuery(user.sqlConnection,
                $"UPDATE {Vars.TableName} SET " +
                $"{Vars.UserName}=N'{userData[Vars.UserName]}'," +
                $"{Vars.UserLastName}=N'{userData[Vars.UserLastName]}'," +
                //$"{Vars.UserPassword}=N'{userData[Vars.UserPassword]}'," +
                $"{Vars.UserEMail}=N'{userData[Vars.UserEMail]}'," +
                $"{Vars.UserPhone}=N'{userData[Vars.UserPhone]}'," +
                $"{Vars.UserGroup}=N'{userData[Vars.UserGroup]}'," +
                $"{Vars.UserAdmin}='{userData[Vars.UserAdmin]}' " +
                $"WHERE ID={rowId}");
        }

        internal static bool PasswordChange(User user, int rowId, string newPassword)
        {
            return RequestExecuteNonQuery(user.sqlConnection,
                $"UPDATE {Vars.TableName} SET " +
                $"{Vars.UserPassword}=N'{newPassword}'" +
                $"WHERE ID={rowId}");
        }

        /// <summary>
        /// Заменяет все данные в строке на null
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="rowId"></param>
        /// <returns></returns>
        internal static bool RemoveTableRow(User user, int rowId)
        {
            Dictionary<string, string> UserNull = new Dictionary<string, string>()
            {
                { Vars.UserName, "" },
                { Vars.UserLastName, "" },
                { Vars.UserEMail, "" },
                { Vars.UserPhone, "" },
                { Vars.UserGroup, "" } ,
                { Vars.UserPassword, "" },
                { Vars.UserAdmin, "0" }
            };

            return Change(user, rowId, UserNull);
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
