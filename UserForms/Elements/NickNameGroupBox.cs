using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements.Abstracts;
using System.Drawing;

namespace UserData.UserForms.Elements
{
    internal class NickNameGroupBox : GroupBoxTextBox
    {
        public NickNameGroupBox(int X, int Y)
        {
            GroupBoxText = Vars.NickName + ":";
            LabelImage = Properties.Resources.User;
            GroupBoxLocation = new Point(X, Y);
            WatermarkText = $"{Vars.LastName} {Vars.Name}";
            TextBoxMaxLength = Vars.UserTextL;
        }
    }
}
