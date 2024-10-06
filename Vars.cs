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
            UserPhone = "UPhone";

        public static int UserNamesL = 20,
            UserPasswordL = 30,
            UserEMailL = 50,
            UserPhoneL = 17;

        public static string User = "Пользователь",
            EMail = "Эл. почта",
            EmailIncorrect = "Адрес эл. почты заполнен не корректно",
            Phone = "Номер телефона",
            Name = "Имя",
            LastName = "Фамилия",
            NameAndLastName = "Имя Фамилия",
            LoginNameError = "Пользователя с таким именем не существует!",
            PasswordError = "Неправельный пароль!",
            TextBoxEmpty = "Заполните поле",
            Registration = "Регистрация",
            GetRegistration = "Зарегистрироваться",
            User2 = "пользователя",
            Password = "Пароль",
            Authorization = "Авторизация",
            RegistrationText = "Регистрация нового пользователя",
            Enter = "Войти",
            TableNotCreated = "Не удалось создать таблицу с пользователями.",
            UserNorRegistred = "Не удалось зарегистрировать пользователя.",
            UserNotExsistConfirmation = "Пользователь с данным именем не существует. Хотите зарегистрироваться?";

    }
}
