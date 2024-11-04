using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using UserData.MyElements;
using UserData.UserForms;

namespace UserData
{
    internal class UsersTableActions
    {
        DB DataBase { get; set; }

        public UsersTableActions(DB dataBase)
        {
            DataBase = dataBase;
        }

        /// <summary>
        /// Проверяет существование таблицы
        /// </summary>
        /// <returns></returns>
        internal bool UsersTableAvailabilityCheck() { return DataBase.TableCheck(Vars.TableName); }

        /// <summary>
        /// Создает таблицу Users
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        internal bool NewTableCreation()
        {
            User user = new User();

            if (!DataBase.NewTableCreation($"CREATE TABLE {DataBase.DataProvider.DataBase}.{Vars.TableName} " +
                $"(Id INT AUTO_INCREMENT NOT NULL,{GetUsersColumns()}"))
            {
                if (!DataBase.NewTableCreation($"CREATE TABLE dbo.{Vars.TableName} " +
                $"(Id INT IDENTITY(1,1) NOT NULL,{GetUsersColumns()}"))
                { MessageBox.Show("Не удалось создать таблицу с пользователями."); return false; }
            }

            user.UserPassword = "admin";
            user.UserAdmin = "1";
            user.UserName = "admin";
            user.UserMiddleName = " ";
            user.UserLastName = " ";
            user.UserEmail = " ";
            user.UserPhoneNumber = " ";
            user.UserAddress = " ";
            user.UserGroup = " ";

            if (!AddNewUser(user))
                { MessageBox.Show("Таблица Users создана! Добавить администратора не удалось!"); return true; }

            MessageBox.Show("Таблица Users создана! Добавлена учетная запись администратора. Login - admin. Password - admin."); return true;

            string GetUsersColumns()
            {
                return  $"{nameof(user.UserPassword)} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                        $"{nameof(user.UserAdmin)} CHAR(1) NOT NULL," +
                        $"{nameof(user.UserName)} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                        $"{nameof(user.UserMiddleName)} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                        $"{nameof(user.UserLastName)} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                        $"{nameof(user.UserEmail)} NVARCHAR({Vars.UserEMailL}) NOT NULL," +
                        $"{nameof(user.UserPhoneNumber)} NVARCHAR({Vars.UserPhoneL}) NOT NULL," +
                        $"{nameof(user.UserAddress)} NVARCHAR({Vars.UserAddressL}) NOT NULL," +
                        $"{nameof(user.UserGroup)} NVARCHAR({Vars.UserTextL}) NOT NULL," +
                        $"primary key (id))";
            }
        }

        /// <summary>
        /// Добавляет пользователя в таблицу
        /// </summary>
        /// <param name="dataBase"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        internal bool AddNewUser(User user)
        {
            return DataBase.AddTableRow($"INSERT INTO {Vars.TableName} " +
                $"(" +
                $"{nameof(user.UserPassword)}, " +
                $"{nameof(user.UserAdmin)}, " +
                $"{nameof(user.UserName)}, " +
                $"{nameof(user.UserMiddleName)}, " +
                $"{nameof(user.UserLastName)}, " +
                $"{nameof(user.UserEmail)}, " +
                $"{nameof(user.UserPhoneNumber)}, " +
                $"{nameof(user.UserAddress)}, " +
                $"{nameof(user.UserGroup)}" +
                $") VALUES (" +
                $"N'{user.UserPassword}', " +
                $"N'{user.UserAdmin}', " +
                $"N'{user.UserName}', " +
                $"N'{user.UserMiddleName}', " +
                $"N'{user.UserLastName}', " +
                $"N'{user.UserEmail}', " +
                $"N'{user.UserPhoneNumber}', " +
                $"N'{user.UserAddress}', " +
                $"N'{user.UserGroup}'" +
                $")");
        }

        /// <summary>
        /// Читает таблицу Users в DataTable
        /// </summary>
        /// <returns></returns>
        internal DataTable ReadUsersDataTable() { return DataBase.ReadDataTable(Vars.TableName); }

        /// <summary>
        /// Заменяет данные в строке
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rowId"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        internal bool ChangeUserData(User user)
        {
            return DataBase.ChangeTableRow($"UPDATE {Vars.TableName} SET " +
                $"{nameof(user.UserPassword)}=N'{user.UserPassword}'," +
                $"{nameof(user.UserAdmin)}=N'{user.UserAdmin}'," +
                $"{nameof(user.UserName)}=N'{user.UserName}'," +
                $"{nameof(user.UserMiddleName)}=N'{user.UserMiddleName}'," +
                $"{nameof(user.UserLastName)}=N'{user.UserLastName}'," +
                $"{nameof(user.UserEmail)}=N'{user.UserEmail}'," +
                $"{nameof(user.UserPhoneNumber)}=N'{user.UserPhoneNumber}'," +
                $"{nameof(user.UserAddress)}=N'{user.UserAddress}'," +
                $"{nameof(user.UserGroup)}='{user.UserGroup}' " +
                $"WHERE ID={user.Id}");
        }




















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
