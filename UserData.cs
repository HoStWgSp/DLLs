using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UserData.UserForms;
using UserData.UserForms.Elements;
using static System.Net.Mime.MediaTypeNames;

namespace UserData
{
    public class UserData
    {
        // Объект ДБ
        internal DB dataBase;
        // Объект User
        public User User { get; internal set; }

        //public DataTable UsersDataTable { get; internal set; }

        //internal UserActions UserActions { get; private set; }

        //internal UserListForm userListForm;

        internal Icon formIcon;
        public Icon FormIcon { set { formIcon = value; } }

        //internal string[] groupsList;
        //public List<string> GroupsList { set { groupsList = value.ToArray(); } }

        //public Dictionary<string, string> NewUserData { get; internal set; }
        //public Dictionary<string, string> UsersccData { get; internal set; }

        //internal bool Admin { get; set; } = false;

        public UserData(DB db)
        {
            // копирует объект БД
            dataBase = db;
            // Создаем экземпляр User
            User = new User();
        }

        /// <summary>
        /// Проверяет наличие таблицы с данными пользователей. Возвращает true, если есть
        /// </summary>
        public bool UsersDataTableAvailable() { return dataBase.TableCheck(Vars.TableName); }

        /// <summary>
        /// Создает таблицу с данными пользователей. Возвращает true при удачном создании
        /// </summary>
        public bool NewUserDataTableCreation() { return UsersTableActions.NewTableCreation(dataBase); } 

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        public bool AddUser (string userGroup, string formTaxt)
        {
            AddNewUserForm addNewUserForm = new AddNewUserForm(this, new string[] { userGroup });//, true);
            return addNewUserForm.UserAdded;
        }






        // ------------------------------------------------------------------
        /// <summary>
        /// Создает объект Пользователь.
        /// Проверяет существует ли таблица пользователей.
        /// Если не существует, то предлагает создать.
        /// На выходе переменная TableExsist.
        /// </summary>
        public UserData(DB db, Dictionary<int, string> groups)
        {
            //// Список с уровнями привилегий
            //Groups = groups;
            ////Testos();
            //// копирует объект БД
            //dataBase = db;
            //// Проверяет существование таблицы
            //if (!dataBase.TableCheck(Vars.TableName))
            //{
            //    // Если нет, то создаем
            //    if (!UsersTableActions.NewTableCreation(db))
            //        MessageBox.Show(Vars.TableNotCreated);
            //    else
            //    {
            //        AlertAndConfirm.AlertConfirmForm alertAndConfirm = new AlertAndConfirm.AlertConfirmForm();
            //        alertAndConfirm.Button1Text = "Заполнить";
            //        alertAndConfirm.Confirmation("Это первый рапуск программы. Для продолжения пожалуйста заполните данные " +
            //            "владельца компании!", 350, 120);

            //        if (!alertAndConfirm.Confirm)
            //            return;

            //        AddNewUserForm addNewUserForm = new AddNewUserForm(this, true);
            //    }
            //}

            //UserActions = new UserActions(this);

            //UsersccData = new Dictionary<string, string>();
            //NewUserData = new Dictionary<string, string>();


            //UsersData = dataBase.ReadDataTable(Vars.TableName);

            //UserAuthorization(true);
        }

        private void Testos()
        {
            //AddNewUserForm addNewUserForm = new AddNewUserForm(this);
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
