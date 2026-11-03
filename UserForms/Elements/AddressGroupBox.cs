using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements.Abstracts;

namespace UserData.UserForms.Elements
{
    internal class AddressGroupBox : GroupBoxTextBox
    {
        public AddressGroupBox(int X, int Y)
        {
            GroupBoxText = Vars.Address + ":";
            LabelImage = Properties.Resources.Address;
            GroupBoxLocation = new Point(X, Y);
            WatermarkText = Vars.Address;
            TextBoxMaxLength = 500;
        }
    }
}
