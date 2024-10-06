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
        public SqlConnection sqlConnection;
        public MainForm mainForm;
        public DataTable usersData;
        public Dictionary<string, string> NewUserData { get; internal set; }
        public Dictionary<string, string> UserData { get; internal set; }

        public bool TableExsist { get; private set; } = true;

        /// <summary>
        /// Создает объект Пользователь.
        /// Проверяет существует ли таблица пользователей.
        /// Если не существует, то предлагает создать.
        /// На выходе переменная TableExsist.
        /// </summary>
        /// <param name="sqlConnection"></param>
        public User(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
            mainForm = new MainForm(Properties.Resources.IconLogIn);

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

            UserData = new Dictionary<string, string>();
            NewUserData = new Dictionary<string, string>();
        }

        /// <summary>
        /// Проверка существование пользователя.
        /// Если пользователь авторизован, его данные будут храниться в User.UserData
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool UserAuthorization(bool registration = false)
        { 
            UserActions.Authorization(this, registration);
            mainForm.ShowDialog();
            UserData = new Dictionary<string, string>(NewUserData);
            return mainForm.mainPanel.UserAuthorized;
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <returns></returns>
        public bool UserRegistration()
        {
            UserActions.Registration(this);
            mainForm.ShowDialog();
            return mainForm.mainPanel.UserAuthorized;
        }
    }
}
