using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using WorkWithUser.UserForms;
using System.Windows.Forms;

namespace WorkWithUser
{
    public class User
    {
        internal SqlConnection sqlConnection;
        internal MainForm mainForm;
        internal DataTable usersData;
        internal string[] groupList;
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
        public User(SqlConnection sqlConnection, List<string> groupList)
        {
            this.sqlConnection = sqlConnection;
            mainForm = new MainForm(Properties.Resources.IconLogIn);
            this.groupList = groupList.ToArray();
            UserData = new Dictionary<string, string>();
            NewUserData = new Dictionary<string, string>();

            // Проверяет существование таблицы пользователей
            if (!WorkWithDB.DBTableCheck.TableCheck(sqlConnection, Vars.TableName))
            {
                // Создание таблицы с пользователями
                if (!UsersTableActions.Creation(sqlConnection)) { MessageBox.Show("Не удалось создать таблицу с пользователями."); { TableExsist = false; return; } }
                else { MessageBox.Show("Таблица Users создана!"); }
            }

            usersData = WorkWithDB.RequestToSQL.ExecuteReaderToDataTable(sqlConnection,
                    $"select * from {Vars.TableName}");
        }

        /// <summary>
        /// Проверка существование пользователя.
        /// Если пользователь авторизован, его данные будут храниться в User.UserData
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public Dictionary<string, string> UserAuthorization(bool registration = false)
        { 
            UserActions.Authorization(this, registration);
            mainForm.ShowDialog();
            UserData = new Dictionary<string, string>(NewUserData);
            return UserData;
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> AddNewUser()
        {
            UserActions.AddNewUser(this);
            mainForm.ShowDialog();
            return NewUserData;
        }
    }
}
