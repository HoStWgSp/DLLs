using DataBase;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UserData
{
    internal class UsersTableActions
    {
        /// <summary>
        /// Создает таблицу Users
        /// </summary>
        internal static bool NewTableCreation(DB db, string tableName)
        {
            List<Tuple<string, string, int, string>> tuples = new List<Tuple<string, string, int, string>>
            {
                new Tuple<string, string, int, string>(
                    Vars.UserPassword, "NVARCHAR", Vars.UserTextL, "NOT NULL"),
                new Tuple<string, string, int, string>(
                    Vars.UserNickName, "NVARCHAR", Vars.UserTextL*2+1, "NOT NULL"),
                new Tuple<string, string, int, string>(
                    Vars.UserName, "NVARCHAR", Vars.UserTextL, "NOT NULL"),
                new Tuple<string, string, int, string>(
                    Vars.UserMiddleName, "NVARCHAR", Vars.UserTextL, "NOT NULL"),
                new Tuple<string, string, int, string>(
                    Vars.UserLastName, "NVARCHAR", Vars.UserTextL, "NOT NULL"),
                new Tuple<string, string, int, string>(
                    Vars.UserEMail, "NVARCHAR", Vars.UserEMailL, "NOT NULL"),
                new Tuple<string, string, int, string>(
                    Vars.UserPhone, "NVARCHAR", Vars.UserPhoneL, "NOT NULL"),
                new Tuple<string, string, int, string>(
                    Vars.UserAddress, "NVARCHAR", Vars.UserAddressL, "NOT NULL"),
                new Tuple<string, string, int, string>(
                    Vars.UserGroup, "NVARCHAR", Vars.UserTextL, "NOT NULL")
            };

            if (!db.NewTableCreation(tableName, tuples))
                {
                MessageBox.Show(Vars.TableNotCreated);
                return false;
                }
            else
                return true;
        }

        /// <summary>
        /// Добавляет пользователя в таблицу
        /// </summary>
        internal static bool AddNewUser(UserData userData, User user)
        {
            List<Tuple<string, string>> tuples = new List<Tuple<string, string>>
            {
                new Tuple<string, string>(
                    Vars.UserPassword, user.UserPassword),
                new Tuple<string, string>(
                    Vars.UserNickName, $"{user.UserLastName} {user.UserName}"),
                new Tuple<string, string>(
                    Vars.UserName, user.UserName),
                new Tuple<string, string>(
                    Vars.UserMiddleName, user.UserMiddleName),
                new Tuple<string, string>(
                    Vars.UserLastName, user.UserLastName),
                new Tuple<string, string>(
                    Vars.UserEMail, user.UserEmail),
                new Tuple<string, string>(
                    Vars.UserPhone, user.UserPhoneNumber),
                new Tuple<string, string>(
                    Vars.UserAddress, user.UserAddress),
                new Tuple<string, string>(
                    Vars.UserGroup, user.UserGroup),
            };
            if (userData.DB.AddTableRow(userData.tableName, tuples)) return true;

            // Если не удалось добавить пользователя
            MessageBox.Show(Vars.UserNorRegistred);
            return false; 
        }

        /// <summary>
        /// Читает таблицу Users в DataTable
        /// </summary>
        /// <returns></returns>
        //internal DataTable ReadUsersDataTable() { return DataBase.ReadDataTable(Vars.TableName); }

        /// <summary>
        /// Заменяет данные в строке
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rowId"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        //internal static bool ChangeUserData(DB db, User user)
        //{
        //    return db.ChangeTableRow($"UPDATE {Vars.TableName} SET " +
        //        $"{Vars.UserPassword}=N'{user.UserPassword}'," +
        //        $"{Vars.UserName}=N'{user.UserName}'," +
        //        $"{Vars.UserMiddleName}=N'{user.UserMiddleName}'," +
        //        $"{Vars.UserLastName}=N'{user.UserLastName}'," +
        //        $"{Vars.UserEMail}=N'{user.UserEmail}'," +
        //        $"{Vars.UserPhone}=N'{user.UserPhoneNumber}'," +
        //        $"{Vars.UserAddress}=N'{user.UserAddress}'," +
        //        $"{Vars.UserGroup}='{user.UserGroup}' " +
        //        $"WHERE ID={user.Id}");
        //}




















        ///// <summary>
        ///// Производит поиск строк в таблице и возвращает DataTable со списком данных строк.
        ///// </summary>
        ///// <param name="table"></param>
        ///// <param name="request"></param>
        ///// <param name="connectinString"></param>
        //public static DataTable ExecuteReaderToDataTable(UserData user, string QueryString)
        //{
        //    SqlCommand sqlCommand = new SqlCommand(QueryString, user.dataBase.SqlConnection);

        //    SqlDataReader dr = sqlCommand.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    return dt;
        //}




        ///// <summary>
        ///// Добавляет пользователя в таблицу пользователей.
        ///// </summary>
        ///// <param name="sqlConnection"></param>
        ///// <param name="userData"></param>
        ///// <returns></returns>
        //internal static bool Add(UserData user, Dictionary<string, string> userData)
        //{
        //    foreach (DataRow dataRow in user.UsersData.Rows)
        //    {
        //        if ((dataRow[Vars.UserName].ToString() == null  || dataRow[Vars.UserName].ToString() == "") && 
        //            (dataRow[Vars.UserLastName].ToString() == null || dataRow[Vars.UserLastName].ToString() == ""))
        //        {
        //            return Change(user, Convert.ToInt32(dataRow["Id"]), userData);
        //        }
        //    }
        //    return RequestExecuteNonQuery(user.dataBase.SqlConnection,
        //        $"INSERT INTO {Vars.TableName} (" +
        //        $"{Vars.UserName}, " +
        //        $"{Vars.UserLastName}, " +
        //        $"{Vars.UserPassword}, " +
        //        $"{Vars.UserEMail}, " +
        //        $"{Vars.UserPhone}, " +
        //        $"{Vars.UserGroup}, " +
        //        $"{Vars.UserAdmin}" +
        //        $") VALUES (" +
        //        $"N'{userData[Vars.UserName]}', " +
        //        $"N'{userData[Vars.UserLastName]}', " +
        //        $"N'{userData[Vars.UserPassword]}', " +
        //        $"N'{userData[Vars.UserEMail]}', " +
        //        $"N'{userData[Vars.UserPhone]}', " +
        //        $"N'{userData[Vars.UserGroup]}', " +
        //        $"'{userData[Vars.UserAdmin]}')"
        //        );
        //}



        //internal static bool PasswordChange(UserData user, int rowId, string newPassword)
        //{
        //    return RequestExecuteNonQuery(user.dataBase.SqlConnection,
        //        $"UPDATE {Vars.TableName} SET " +
        //        $"{Vars.UserPassword}=N'{newPassword}'" +
        //        $"WHERE ID={rowId}");
        //}

        ///// <summary>
        ///// Заменяет все данные в строке на null
        ///// </summary>
        ///// <param name="sqlConnection"></param>
        ///// <param name="rowId"></param>
        ///// <returns></returns>
        //internal static bool RemoveTableRow(UserData user, int rowId)
        //{
        //    Dictionary<string, string> UserNull = new Dictionary<string, string>()
        //    {
        //        { Vars.UserName, "" },
        //        { Vars.UserLastName, "" },
        //        { Vars.UserEMail, "" },
        //        { Vars.UserPhone, "" },
        //        { Vars.UserGroup, "" } ,
        //        { Vars.UserPassword, "" },
        //        { Vars.UserAdmin, "0" }
        //    };

        //    return Change(user, rowId, UserNull);
        //}

        ///// <summary>
        ///// Отправляет команду в SQL DataBase и выполняет ExecuteNonQuery()
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //private static bool RequestExecuteNonQuery(SqlConnection sqlConnection, string queryString)
        //{
        //    SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
        //    try
        //    {
        //        sqlCommand.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.ToString());
        //        return false;
        //    }
        //}
    }
}
