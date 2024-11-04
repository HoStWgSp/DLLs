using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UserData.UserForms;
using System.Windows.Forms;
using System.Drawing;
using MySql.Data.MySqlClient;
using DataBase;
using UserData.UserForms.Elements;

namespace UserData
{
    public class UserData
    {
        public DB DataBase;

        public DataTable UsersData { get; internal set; }
        public User User { get; set; } = new User();

        internal UsersTableActions usersTableActions;
        internal UserActions userActions;

        internal UserListForm userListForm;

        internal Icon formIcon;
        public Icon FormIcon { set { formIcon = value; } }

        internal string[] groupsList;
        public List<string> GroupsList { set { groupsList = value.ToArray(); } }

        public Dictionary<string, string> NewUserData { get; internal set; }
        public Dictionary<string, string> UsersccData { get; internal set; }

        public bool TableExsist { get; private set; } = true;
        internal bool Admin { get; set; } = false;

        /// <summary>
        /// Создает объект Пользователь.
        /// Проверяет существует ли таблица пользователей.
        /// Если не существует, то предлагает создать.
        /// На выходе переменная TableExsist.
        /// </summary>
        /// <param name="sqlConnection"></param>
        public UserData(string connectionString)
        {
            DataBase = new DB(formIcon, connectionString);
            usersTableActions = new UsersTableActions(DataBase);
            userActions = new UserActions(this);
        }
        public UserData(DB dataBase)
        {
            DataBase = dataBase;
            usersTableActions = new UsersTableActions(DataBase);
            userActions = new UserActions(this);

            //UsersccData = new Dictionary<string, string>();
            //NewUserData = new Dictionary<string, string>();


            //UsersData = dataBase.ReadDataTable(Vars.TableName);

            //UserAuthorization(true);
        }
        /// <summary>
        /// Проверяет существование таблицы
        /// </summary>
        /// <returns></returns>
        public bool UsersTableAvailability() { return usersTableActions.UsersTableAvailabilityCheck(); }

        /// <summary>
        /// Создает таблицу Users
        /// </summary>
        /// <returns></returns>
        public bool CreateNewUsersTable() { return usersTableActions.NewTableCreation(); }



        /// <summary>
        /// Авторизация пользователя. Вернет true, если авторизован.
        /// Если пользователь авторизован, его данные будут храниться в User
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool UserAuthorization()
        {
            UsersData = usersTableActions.ReadUsersDataTable();
            UserLogInForm userLogInForm = new UserLogInForm(this);
            return userLogInForm.Authorized;
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <returns></returns>
        public bool AddNewUser(bool admin = false)
        {
            UsersData = usersTableActions.ReadUsersDataTable();
            AddNewUserForm newUserForm = new AddNewUserForm(this, Vars.NewUser, Vars.Add, admin);
            return true;
        }







        //public UserData(MySqlConnection mySqlConnection, Icon formIcon, List<string> groupList)
        //{

        //}









        ///// <summary>
        ///// Отображает данные авторизованного пользователя
        ///// </summary>
        //public void UserPersonalData()
        //{
        //    userForm = new UserForm(formIcon);
        //    UserActions.UserData(this, Vars.Change, UsersccData);
        //    userForm.ShowDialog();
        //}

        ///// <summary>
        ///// Показывает таблицу со списком валидных пользователей.
        ///// Открывает только админам.
        ///// </summary>
        //public void UserList()
        //{
        //    if (!Admin) return;
        //    userListForm = new UserListForm(formIcon);
        //    UsersData = UsersTableActions.ExecuteReaderToDataTable(this,
        //            $"select * from {Vars.TableName}");
        //    UserActions.UserList(this);
        //    userListForm.ShowDialog();
        //}








        ///// <summary>
        ///// Отображает данные пользователя выбранного админом.
        ///// </summary>
        ///// <param name="userData"></param>
        //internal void UserPersonalData(Dictionary<string, string> userData)
        //{
        //    userForm = new UserForm(formIcon);
        //    UserActions.UserData(this, Vars.Change, userData);
        //    userForm.ShowDialog();
        //}
    }
}
