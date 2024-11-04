using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UserData.MyElements;
using UserData.UserForms;

namespace UserData
{
    internal class UserActions
    {
        UserData UserData { get; set; }
        public UserActions(UserData userData)
        {
            UserData = userData;
        }

        public User UserFromTable(int userId)
        {
            foreach (DataRow row in UserData.UsersDataTable.Rows)
            {
                if (Convert.ToInt32(row[0]) == userId)
                {
                    return new User()
                    {
                        Id = Convert.ToInt32(row[nameof(User.Id)]),
                        UserAdmin = row[nameof(User.UserAdmin)].ToString(),
                        UserName = row[nameof(User.UserName)].ToString(),
                        UserMiddleName = row[nameof(User.UserMiddleName)].ToString(),
                        UserLastName = row[nameof(User.UserLastName)].ToString(),
                        UserEmail = row[nameof(User.UserEmail)].ToString(),
                        UserPhoneNumber = row[nameof(User.UserPhoneNumber)].ToString(),
                        UserAddress = row[nameof(User.UserAddress)].ToString(),
                        UserGroup = row[nameof(User.UserGroup)].ToString()
                    };
                }
            }
            return null;
        }
    }
}
