using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements.Abstracts;

namespace UserData.UserForms.Elements
{
    internal class AddressGroupBox : GroupBoxTextBox
    {
        public AddressGroupBox(int X, int Y):
            base(Vars.Address, Properties.Resources.Address, X, Y, Vars.Address, 500)
        {

        }
    }
}
