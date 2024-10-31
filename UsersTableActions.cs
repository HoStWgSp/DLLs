using DataBase;
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
        /// Создает таблицу Users
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        internal static bool NewTableCreation(DataBase.DataBase dataBase, User user)
        {
            return dataBase.RequestExecuteNonQuery($"CREATE TABLE mytest.{Vars.TableName} " +
                $"(Id INT AUTO_INCREMENT NOT NULL ," +
                $"{Vars.UserNickName} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"{Vars.UserPassword} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"{Vars.UserAdmin} CHAR(1) NOT NULL," +
                $"{Vars.UserName} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"{Vars.UserMiddleName} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"{Vars.UserLastName} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"{Vars.UserEMail} NVARCHAR({Vars.UserEMailL}) NOT NULL," +
                $"{Vars.UserPhone} NVARCHAR({Vars.UserPhoneL}) NOT NULL," +
                $"{Vars.UserAddress} NVARCHAR({Vars.UserAddressL}) NOT NULL," +
                $"{Vars.UserGroup} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                $"primary key (id))");
        }

        /// <summary>
        /// Добавляет пользователя в таблицу
        /// </summary>
        /// <param name="dataBase"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static bool AddNewUser(DataBase.DataBase dataBase, User user)
        {
            return dataBase.RequestExecuteNonQuery($"INSERT INTO {Vars.TableName} " +
                $"({Vars.UserNickName}, " +
                $"{Vars.UserPassword}, " +
                $"{Vars.UserAdmin}, " +
                $"{Vars.UserName}, " +
                $"{Vars.UserMiddleName}, " +
                $"{Vars.UserLastName}, " +
                $"{Vars.UserEMail}, " +
                $"{Vars.UserPhone}, " +
                $"{Vars.UserAddress}, " +
                $"{Vars.UserGroup}" +
                $") VALUES (" +
                $"N'{user.UserNickName}', " +
                $"N'{user.UserPassword}', " +
                $"N'{user.UserAdmin}', " +
                $"N'{user.UserName}', " +
                $"N'{user.UserMiddleName}', " +
                $"N'{user.UserLastName}', " +
                $"N'{user.UserEmail}', " +
                $"N'{user.UserPhoneNumber}', " +
                $"N'{user.UserAddress}', " +
                $"N'{user.UserGroup}')");
        }

        /// <summary>
        /// Производит поиск строк в таблице и возвращает DataTable со списком данных строк.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="request"></param>
        /// <param name="connectinString"></param>
        public static DataTable ExecuteReaderToDataTable(UserData user, string QueryString)
        {
            SqlCommand sqlCommand = new SqlCommand(QueryString, user.dataBase.SqlConnection);

            SqlDataReader dr = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }

        
        

        /// <summary>
        /// Добавляет пользователя в таблицу пользователей.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        internal static bool Add(UserData user, Dictionary<string, string> userData)
        {
            foreach (DataRow dataRow in user.UsersData.Rows)
            {
                if ((dataRow[Vars.UserName].ToString() == null  || dataRow[Vars.UserName].ToString() == "") && 
                    (dataRow[Vars.UserLastName].ToString() == null || dataRow[Vars.UserLastName].ToString() == ""))
                {
                    return Change(user, Convert.ToInt32(dataRow["Id"]), userData);
                }
            }
            return RequestExecuteNonQuery(user.dataBase.SqlConnection,
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
        internal static bool Change(UserData user, int rowId, Dictionary<string, string> userData)
        {
            return RequestExecuteNonQuery(user.dataBase.SqlConnection,
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

        internal static bool PasswordChange(UserData user, int rowId, string newPassword)
        {
            return RequestExecuteNonQuery(user.dataBase.SqlConnection,
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
        internal static bool RemoveTableRow(UserData user, int rowId)
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
