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
            UserName = "Name",
            UserLastName = "LastName",
            UserPassword = "Password",
            UserEMail = "EMail",
            UserGroup = "Level";

        public static int UserNames = 20,
            UserPasswordL = 30,
            UserEMailL = 30,
            UserGroupL = 10;

        public static string 
            TableNotCreated = "Не удалось создать таблицу с пользователями.";
    }
}
