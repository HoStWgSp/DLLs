using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements.Abstracts;

namespace UserData.UserForms.Elements
{
    internal class NickNameGroupBox : GroupBoxTextBox
    {
        public NickNameGroupBox(int X, int Y) :
            base(Vars.NickName + ":", Properties.Resources.User, X, Y, $"{Vars.LastName} {Vars.Name}", Vars.UserTextL)
        {

        }
    }
}
