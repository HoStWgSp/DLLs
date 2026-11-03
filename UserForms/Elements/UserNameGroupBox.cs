using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements.Abstracts;

namespace UserData.UserForms.Elements
{
    internal class UserNameGroupBox : GroupBoxTextBox
    {
        public UserNameGroupBox(int X, int Y)
        {
            GroupBoxText = Vars.User + ":";
            LabelImage = Properties.Resources.User;
            GroupBoxLocation = new Point(X, Y);
            WatermarkText = Vars.User;
            TextBoxMaxLength = Vars.UserTextL;
        }
    }
}
