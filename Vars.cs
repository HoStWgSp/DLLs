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

        public static int UserNames = 20,
            UserPasswordL = 30,
            UserEMailL = 30,
            UserGroupL = 10;

        public static string 
            TableNotCreated = "Не удалось создать таблицу с пользователями.";
    }
}
