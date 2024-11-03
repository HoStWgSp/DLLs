using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements;

namespace UserData.UserForms
{
    internal class AddNewUserForm : UserDataFormTemplate
    {
        private UserData UserData { get; set; }

        public AddNewUserForm(UserData userData, string labelText, string buttonText, bool addAdmin = false):
            base(userData.formIcon, userData.groupsList, labelText, buttonText, addAdmin)
        {
            UserData = userData;
            ShowDialog();
        }

        internal override void Button_Click(object sender, EventArgs e)
        {
            if (!TextBoxesNotEmptyOrWaterMarked()) return;
        }
    }
}
