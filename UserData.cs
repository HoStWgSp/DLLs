using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UserData.UserForms;
using System.Windows.Forms;
using System.Drawing;
using MySql.Data.MySqlClient;
using DataBase;

namespace UserData
{
    public class UserData
    {
        internal DataBase.DataBase dataBase;
        public User User { get; set; } = new User();

        internal UserForm userForm;
        internal UserListForm userListForm;
        internal Icon formIcon;
        public DataTable UsersData {  get; internal set; }
        internal string[] groupList;
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
        public UserData(DataBase.DataBase dataBase, Icon formIcon, List<string> groupList)
        {
            this.dataBase = dataBase;
            this.formIcon = formIcon;
            this.groupList = groupList.ToArray();

            //UsersccData = new Dictionary<string, string>();
            //NewUserData = new Dictionary<string, string>();

            // Проверяет существование таблицы пользователей
            if (dataBase.TableCheck(Vars.TableName) == false)
            {
                // Создание таблицы с пользователями
                if (!UsersTableActions.NewTableCreation(dataBase,User))
                {
                    MessageBox.Show("Не удалось создать таблицу с пользователями.");
                    TableExsist = false;
                    return;
                }
                else
                {
                    MessageBox.Show("Таблица Users создана!");

                    //попробовать пустые строки

                    User.UserNickName = " ";
                    User.UserPassword = "admin";
                    User.UserAdmin = "1";
                    User.UserName = "admin";
                    User.UserMiddleName = " ";
                    User.UserLastName = " ";
                    User.UserEmail = " ";
                    User.UserPhoneNumber = " ";
                    User.UserAddress = " ";
                    User.UserGroup = " ";

                    if (!UsersTableActions.AddNewUser(dataBase, User))
                    {
                        MessageBox.Show("Не удалось добавить пользователя в таблицу.");
                    }
                }
            } 

            UsersData = UsersTableActions.ExecuteReaderToDataTable(this,
                    $"select * from {Vars.TableName}");

            UserAuthorization(true);
        }
        public UserData(MySqlConnection mySqlConnection, Icon formIcon, List<string> groupList)
        {

        }









        /// <summary>
        /// Отображает данные авторизованного пользователя
        /// </summary>
        public void UserPersonalData()
        {
            userForm = new UserForm(formIcon);
            UserActions.UserData(this, Vars.Change, UsersccData);
            userForm.ShowDialog();
        }

        /// <summary>
        /// Показывает таблицу со списком валидных пользователей.
        /// Открывает только админам.
        /// </summary>
        public void UserList()
        {
            if (!Admin) return;
            userListForm = new UserListForm(formIcon);
            UsersData = UsersTableActions.ExecuteReaderToDataTable(this,
                    $"select * from {Vars.TableName}");
            UserActions.UserList(this);
            userListForm.ShowDialog();
        }




        /// <summary>
        /// Проверка существование пользователя. Вернет true, если авторизован.
        /// Если пользователь авторизован, его данные будут храниться в User.UserData
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        internal bool UserAuthorization(bool registration = false)
        {
            userForm = new UserForm(formIcon);
            UserActions.Authorization(this, registration);
            userForm.ShowDialog();
            UsersccData = new Dictionary<string, string>(NewUserData);
            if (UsersccData.Count == 0) return false;
            if (UsersccData[Vars.UserAdmin] == "1") Admin = true;
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

        /// <summary>
        /// Отображает данные пользователя выбранного админом.
        /// </summary>
        /// <param name="userData"></param>
        internal void UserPersonalData(Dictionary<string, string> userData)
        {
            userForm = new UserForm(formIcon);
            UserActions.UserData(this, Vars.Change, userData);
            userForm.ShowDialog();
        }
    }
}
