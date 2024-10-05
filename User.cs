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
        public DataTable usersData;
        public MainForm mainForm;

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

            mainForm = new MainForm(formIcon);
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
        /// Иконка на форму обязательно (формат ico).
        /// Картинки на пользователя желательно, но не обязательно. (Формат png).
        /// Registration - true если вы хотите, что бы на форме была ссылка на регистрацию.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public bool Aauthorization(Image userImage = null, Image passwordImage = null, bool registration = false)
        {
            AlertConfirm.AlertConfirm alertConfirm = new AlertConfirm.AlertConfirm();

            mainForm.Text = Vars.Authorization + " " + Vars.User2;
            mainForm.Controls.Clear();
            mainForm.mainPanel = new LogInPanel(this, userImage, passwordImage, registration);
            mainForm.Controls.Add(mainForm.mainPanel);
            mainForm.ShowDialog();

            if (mainForm.mainPanel.userDataRow != null)
            {
                UserName = mainForm.mainPanel.userDataRow[Vars.UserName].ToString();
                UserLastName = mainForm.mainPanel.userDataRow[Vars.UserLastName].ToString();
                UserEMail = mainForm.mainPanel.userDataRow[Vars.UserEMail].ToString();
                UserGroup = mainForm.mainPanel.userDataRow[Vars.UserGroup].ToString();
                return true;
            }

            return false;
        }

        public bool Registration()
        {



            if (!Add("", "", "", "", "")) 
                return false;

            
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
