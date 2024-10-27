using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace UserData.UserForms
{
    internal class UserLogInPanel : UserMainPanel
    {
        //User user;

        bool registration;

        /// <summary>
        /// Панель для проверки авторизации пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="registration"></param>
        public UserLogInPanel(UserData user, bool registration):
            base(user, Vars.Authorization, Vars.Enter)
        {
            //this.user = user;
            this.registration = registration;


            //LoginLabel(Vars.Authorization);
            //UserGroupBox();
            //PasswordGroupBox(true);
            //ErrorLabel();
            errorLabel.Location = new Point(41, 155);

            //ButtonBody(Vars.Enter);
            button.Location = new Point(92, 175);

            if (!registration) return;
            RegistrationLabel();
        }



        internal override void Button_Click(object sender, EventArgs e)
        {
            if (!UserTextBoxOK()) return;
            if (!PasswordTextBoxOK()) return;

            DataRow userDataRow = Aauthorization(
                userGroupBox.Controls["textBox"].Text,
                passwordGroupBox.Controls["textBox"].Text);

            if (userDataRow == null) return;

            user.NewUserData.Add(Vars.UserName, userDataRow[Vars.UserName].ToString());
            user.NewUserData.Add(Vars.UserLastName, userDataRow[Vars.UserLastName].ToString());
            user.NewUserData.Add(Vars.UserEMail, userDataRow[Vars.UserEMail].ToString());
            user.NewUserData.Add(Vars.UserPhone, userDataRow[Vars.UserPhone].ToString());
            user.NewUserData.Add(Vars.UserGroup, userDataRow[Vars.UserGroup].ToString());
            user.NewUserData.Add(Vars.UserAdmin, userDataRow[Vars.UserAdmin].ToString());

            user.userForm.Close();
        }

        /// <summary>
        /// Проверяет существует ли пользователь с таким именем.
        /// </summary>
        /// <param name="usersDataTable"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckUserName(string userName)
        {
            foreach (DataRow dataRow in user.UsersData.Rows)
            {
                if (dataRow[Vars.UserName].ToString() == userName) return true;
                else if ((dataRow[Vars.UserName].ToString() + " " + dataRow[Vars.UserLastName].ToString()) == userName) return true;
                else if (dataRow[Vars.UserLastName].ToString() == userName) return true;
            }
            return false;
        }

        /// <summary>
        /// Проверяет существование пользователя в DataTable. И возвращает строку с данными найденного пользователя.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public DataRow Aauthorization(string userName, string password)
        {
            // Проверка существования пользователя            
            foreach (DataRow row in user.UsersData.Rows)
            {
                if (row[Vars.UserName].ToString() == userName ||
                    (row[Vars.UserName].ToString() + " " + row[Vars.UserLastName].ToString()) == userName ||
                    row[Vars.UserLastName].ToString() == userName)
                {
                    if (row[Vars.UserPassword].ToString() == password)
                        return row;
                    else
                    {
                        passwordGroupBox.Controls["textBox"].ForeColor = Color.Red;
                        errorLabel.Text = Vars.PasswordError;
                        return null;
                    }
                }
            }
            userGroupBox.Controls["textBox"].ForeColor = Color.Red;
            errorLabel.Text = Vars.LoginNameError;
            return null;
        }
    }
}
