using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using WorkWithUser.UserForms;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WorkWithUser
{
    public class User
    {
        internal SqlConnection sqlConnection;
        internal UserForm userForm;
        internal UserListForm userListForm;
        internal Icon formIcon;
        public DataTable UsersData {  get; internal set; }
        internal string[] groupList;
        public Dictionary<string, string> NewUserData { get; internal set; }
        public Dictionary<string, string> UserData { get; internal set; }

        public bool TableExsist { get; private set; } = true;
        internal bool Admin { get; set; } = false;

        /// <summary>
        /// Создает объект Пользователь.
        /// Проверяет существует ли таблица пользователей.
        /// Если не существует, то предлагает создать.
        /// На выходе переменная TableExsist.
        /// </summary>
        /// <param name="sqlConnection"></param>
        public User(SqlConnection sqlConnection, Icon formIcon, List<string> groupList)
        {
            this.sqlConnection = sqlConnection;
            this.formIcon = formIcon;
            this.groupList = groupList.ToArray();
            UserData = new Dictionary<string, string>();
            NewUserData = new Dictionary<string, string>();

            // Проверяет существование таблицы пользователей
            if (!UsersTableActions.TableCheck(sqlConnection, Vars.TableName))
            {
                // Создание таблицы с пользователями
                if (!UsersTableActions.Creation(sqlConnection)) { MessageBox.Show("Не удалось создать таблицу с пользователями."); { TableExsist = false; return; } }
                else { MessageBox.Show("Таблица Users создана!"); }
            }

            UsersData = UsersTableActions.ExecuteReaderToDataTable(sqlConnection,
                    $"select * from {Vars.TableName}");
        }

        /// <summary>
        /// Проверка существование пользователя. Вернет true, если авторизован.
        /// Если пользователь авторизован, его данные будут храниться в User.UserData
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool UserAuthorization(bool registration = false)
        {
            userForm = new UserForm(formIcon);
            UserActions.Authorization(this, registration);
            userForm.ShowDialog();
            UserData = new Dictionary<string, string>(NewUserData);
            if (UserData.Count == 0) return false;
            if (UserData[Vars.UserAdmin] == "1") Admin = true;
            return true;
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <returns></returns>
        internal void NewUser()
        {
            userForm = new UserForm(formIcon);
            UserActions.UserData(this, Vars.Add);
            userForm.ShowDialog();
        }

        internal void UserPersonalData(Dictionary<string, string> userData)
        {
            userForm = new UserForm(formIcon);
            UserActions.UserData(this, Vars.Change, userData);
            userForm.ShowDialog();
        }

        /// <summary>
        /// Показывает таблицу со списком валидных пользователей
        /// </summary>
        public void UserList()
        {
            userListForm = new UserListForm(formIcon);
            UsersData = UsersTableActions.ExecuteReaderToDataTable(sqlConnection,
                    $"select * from {Vars.TableName}");
            UserActions.UserList(this);
            userListForm.ShowDialog();
        }
    }
}
