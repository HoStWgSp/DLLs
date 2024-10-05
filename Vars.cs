using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithUser
{
    internal class Vars
    {
        public static string TableName = "Users",
            UserName = "UName",
            UserLastName = "ULastName",
            UserPassword = "UPassword",
            UserEMail = "UEMail",
            UserGroup = "UGroup";

        public static int UserNamesL = 20,
            UserPasswordL = 30,
            UserEMailL = 30,
            UserGroupL = 10;

        public static string User = "Пользователь",
            NameAndLastName = "Имя Фамилия",
            LoginNameError = "Пользователя с таким именем не существует!",
            PasswordError = "Неправельный пароль!",
            LoginEmpty = "Заполните поле",
            Registration = "Зарегистрироваться",
            User2 = "пользователя",
            Password = "Пароль",
            Authorization = "Авторизация",
            Enter = "Войти",
            TableNotCreated = "Не удалось создать таблицу с пользователями.",
            UserNotExsistConfirmation = "Пользователь с данным именем не существует. Хотите зарегистрироваться?";

    }
}
