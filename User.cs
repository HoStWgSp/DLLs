using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using WorkWithUser.UserForms;
using System.Runtime.Versioning;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Security.Policy;
using System.Reflection;

namespace WorkWithUser
{
    public class User
    {
        SqlConnection sqlConnection;
        DataTable usersData;
        DataRow authorizedUser;
        MainForm mainForm;

        public User(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;

            usersData = WorkWithDB.RequestToSQL.ExecuteReaderToDataTable(sqlConnection,
                $"select * from {Vars.TableName}");

            mainForm = new MainForm();
        }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string UserLastName { get; private set; }
        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string UserEMail { get; private set; }
        /// <summary>
        /// Рабочая группа пользователя.
        /// </summary>
        public string UserGroup { get; private set; }

        /// <summary>
        /// Проверяет существование пользователя и возвращает его данные.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public bool Aauthorization(string userName, string UserPassword, Icon icon = null)
        { 
            Сделать иф по наличию icon
            //Icon icon = Icon.ExtractAssociatedIcon("Icons.dll");
            mainForm.Icon = icon;
            mainForm.ShowDialog();
            authorizedUser = UserActions.Aauthorization(usersData, userName, UserPassword);
            if (!authorizedUser.IsNull(1))
            {
                UserName = authorizedUser[Vars.UserName].ToString();
                UserLastName = authorizedUser[Vars.UserLastName].ToString();
                UserEMail = authorizedUser[Vars.UserEMail].ToString();
                UserGroup = authorizedUser[Vars.UserGroup].ToString();
                return true;
            }
            return false;
        }

        public bool Registration(string name, string lastName, string password, string eMail, string group)
        {
            if (!Add(name, lastName, password, eMail, group)) 
                return false;

            UserName = name;
            UserLastName = lastName;
            UserEMail = password;
            UserGroup = group;

            return true;
        }

        /// <summary>
        /// Добавляет нового пользователя в таблицу с пользователями.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="eMail"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool Add(string name, string lastName, string password, string eMail, string group)
        {
            string[] newUserData = { name, lastName, password, eMail, group };
            if (UserActions.Add(sqlConnection, newUserData))
            {
                usersData = WorkWithDB.RequestToSQL.ExecuteReaderToDataTable(sqlConnection,
                $"select * from {Vars.TableName}");
                return true;
            }
            return false;
        }
    }
}
