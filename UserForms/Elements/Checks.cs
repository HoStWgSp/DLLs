using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserData.UserForms.Elements
{
    internal class Checks
    {
        /// <summary>
        /// Проверяет заполнили ли TextBox.
        /// </summary>
        /// <param name="groupBox"></param>
        /// <param name="errorLabel"></param>
        /// <param name="watermarkText"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool TextBoxEmptyOrWaterMarked(TextBox textBox, string watermarkText)
        {
            if (textBox.Text != watermarkText &&
                textBox.Text != "") return false;
            textBox.ForeColor = Color.Red;
            return true;
        }
    }
}
