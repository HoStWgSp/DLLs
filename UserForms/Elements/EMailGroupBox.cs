using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
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
        /// <summary>
        /// проверяет указана ли почта
        /// </summary>
        public bool EMailCorrect
        {
            get { 
            if (TextBoxText == Vars.EMail ||
                TextBoxText == "")
            {
                TextBoxTextColor = Color.Red;
                return false;
            }
            else
            {
                try
                {
                    MailAddress addr = new MailAddress(TextBoxText);
                }
                catch
                {
                    TextBoxTextColor = Color.Red;
                    return false;
                }
            }
            return true;}
        }
    }
}
