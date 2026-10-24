using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements.Abstracts;

namespace UserData.UserForms.Elements
{
    internal class UserNameGroupBox : GroupBoxTextBox
    {
        public UserNameGroupBox(int X, int Y):
            base(Vars.User + ":", Properties.Resources.User, X, Y, Vars.NameAndLastName, Vars.UserTextL)
        {

        }
    }
}
