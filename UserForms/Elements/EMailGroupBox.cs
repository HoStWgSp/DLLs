using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements.Abstracts;

namespace UserData.UserForms.Elements
{
    internal class EMailGroupBox : GroupBoxTextBox
    {
        public EMailGroupBox(int X, int Y):
            base (Vars.EMail + ":", Properties.Resources.EMail,X,Y, Vars.EMail, Vars.UserEMailL)
        {

        }

    }
}
