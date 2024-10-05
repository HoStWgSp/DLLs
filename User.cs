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
using System.Windows.Forms;

namespace WorkWithUser
{
    public class User
    {
        SqlConnection sqlConnection;
        public MainForm mainForm;
        public DataTable usersData;
        Dictionary<string, Image> images;
        public Dictionary<string, string> UserData { get; internal set; }

        public bool TableExsist { get; private set; } = true;

        /// <summary>
        /// Создает объект Пользователь.
        /// Проверяет существует ли таблица пользователей.
        /// Если не существует, то предлагает создать.
        /// На выходе переменная TableExsist.
        /// </summary>
        /// <param name="sqlConnection"></param>
        public User(Icon formIcon, SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
            mainForm = new MainForm(formIcon);

            // Проверяет существование таблицы пользователей
            if (!WorkWithDB.DBTableCheck.TableCheck(sqlConnection, Vars.TableName))
            {
                AlertConfirm.AlertConfirm alertConfirm = new AlertConfirm.AlertConfirm();
                if (!alertConfirm.Confirmation("Таблица с пользователями отсутствует. Хотите создать?", 300, 90)) return;

                // Создание таблицы с пользователями
                if (!UsersTableActions.Creation(sqlConnection)) { MessageBox.Show("Не удалось создать таблицу с пользователями."); return; }
            }

            usersData = WorkWithDB.RequestToSQL.ExecuteReaderToDataTable(sqlConnection,
                $"select * from {Vars.TableName}");

            images = new Dictionary<string, Image>();
            UserData = new Dictionary<string, string>();
        }

        /// <summary>
        /// Наполняет словарь с картинками для окон.
        /// </summary>
        /// <param name="userImage"></param>
        /// <param name="passwordImage"></param>
        public void ImagesFill(Image userImage, Image passwordImage)
        {
            images.Add(Vars.User, userImage);
            images.Add(Vars.Password, passwordImage);
        }

        /// <summary>
        /// Заполняет словарь с данными авторизованного пользователя
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userLastName"></param>
        /// <param name="userEMail"></param>
        /// <param name="userGroup"></param>
        internal void UserDataFill(string userName, string userLastName, string userEMail, string userGroup)
        {
            UserData.Add(Vars.UserName, userName);
            UserData.Add(Vars.UserLastName, userLastName);
            UserData.Add(Vars.UserEMail, userEMail);
            UserData.Add(Vars.UserGroup, userGroup);
        }

        /// <summary>
        /// Проверка существование пользователя.
        /// Если пользователь авторизован, его данные будут храниться в User.UserData
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool UserAuthorization(bool registration = false)
        { return UserActions.Authorization(this, images, registration); }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <returns></returns>
        public bool UserRegistration()
        { return UserActions.Registration(); }
    }
}
