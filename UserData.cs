using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UserData.UserForms;
using System.Windows.Forms;
using System.Drawing;
using DataBase;
using UserData.UserForms.Elements;
using System;

namespace UserData
{
    public class UserData
    {
        // Объект ДБ
        private static DB dataBase;
        // Объект User
        public User User { get; private set; } = new User();

        internal bool UsersTableAvailability {  get; private set; }

        //public DataTable UsersDataTable { get; internal set; }

        //internal UserActions UserActions { get; private set; }

        //internal UserListForm userListForm;

        internal Icon formIcon;
        public Icon FormIcon { set { formIcon = value; } }

        //internal string[] groupsList;
        //public List<string> GroupsList { set { groupsList = value.ToArray(); } }

        //public Dictionary<string, string> NewUserData { get; internal set; }
        //public Dictionary<string, string> UsersccData { get; internal set; }

        public bool TableExsist { get; private set; } = true;
        //internal bool Admin { get; set; } = false;


        // ------------------------------------------------------------------
        /// <summary>
        /// Создает объект Пользователь.
        /// Проверяет существует ли таблица пользователей.
        /// Если не существует, то предлагает создать.
        /// На выходе переменная TableExsist.
        /// </summary>
        public UserData(DB db)
        {
            // копирует объект БД
            dataBase = db;
            // Проверяет существование таблицы
            if (!dataBase.TableCheck(Vars.TableName))
            {
                // Если нет, то создаем
                if (!UsersTableActions.NewTableCreation(db))
                    MessageBox.Show(Vars.TableNotCreated);
                else
                {
                    AlertAndConfirm.AlertConfirmForm alertAndConfirm = new AlertAndConfirm.AlertConfirmForm();
                    alertAndConfirm.Button1Text = "Создать";
                    alertAndConfirm.Confirmation("dsfa", 300, 200);
                }
            }

            //UserActions = new UserActions(this);

            //UsersccData = new Dictionary<string, string>();
            //NewUserData = new Dictionary<string, string>();


            //UsersData = dataBase.ReadDataTable(Vars.TableName);

            //UserAuthorization(true);
        }

        /// <summary>
        /// Создает таблицу Users
        /// </summary>
        /// <returns></returns>
        //public bool CreateNewUsersTable() { return UsersTableActions.NewTableCreation(); }



        /// <summary>
        /// Авторизация пользователя. Вернет true, если авторизован.
        /// Если пользователь авторизован, его данные будут храниться в User
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool UserAuthorization()
        {
            //UsersDataTable = UsersTableActions.ReadUsersDataTable();
            UserLogInForm userLogInForm = new UserLogInForm(this);
            return userLogInForm.Authorized;
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <returns></returns>
        //public bool AddNewUser(bool admin = false)
        //{
            //UsersDataTable = UsersTableActions.ReadUsersDataTable();
            //AddNewUserForm newUserForm = new AddNewUserForm(this, admin);
            //return newUserForm.UserAdded;
        //}

        //public bool Userinfo(int userId, bool admin = false)
        //{
            //UsersDataTable = UsersTableActions.ReadUsersDataTable();
            //User user = UserActions.UserFromTable(userId);
            //UserDataForm userDataForm = new UserDataForm(this, user, admin);
            //return userDataForm.UserDataChanged;
        //}







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
