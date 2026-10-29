using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.UserForms.Elements.Abstracts;

namespace UserData.UserForms.Elements
{
    internal class PhoneNumberGroupBox : GroupBoxMaskedTextBox
    {
        public PhoneNumberGroupBox(int X, int Y):
            base(Vars.Phone + ":", Properties.Resources.Phone, X, Y, "+0(000) 000-00-00")
        {

        }
        /// <summary>
        /// Проверяет указан ли телефон. 
        /// </summary>
        /// <returns></returns>
        internal bool PhoneMaskCompleted
        {
            get
            {
                if (maskedTextBox.MaskCompleted &&
                    maskedTextBox.Text != "+ (   )    -  -") return true;
                maskedTextBox.ForeColor = Color.Red;
                return false;
            }
        }
    }
}
