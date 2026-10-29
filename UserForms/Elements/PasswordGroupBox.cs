using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements.Abstracts;

namespace UserData.UserForms.Elements
{
    internal class PasswordGroupBox : GroupBoxTextBox
    {
        public PasswordGroupBox(int X, int Y) :
            base(Vars.Password + ":", Properties.Resources.Password, X, Y, Vars.Password, Vars.UserTextL)
        {
        }
    }
}
