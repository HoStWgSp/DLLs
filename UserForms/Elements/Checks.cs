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
        public static bool TextBoxNotEmptyOrWaterMarked(TextBox textBox, Label errorLabel, string watermarkText, string errorText)
        {
            if (textBox.Text != watermarkText &&
                textBox.Text != "") return true;
            textBox.ForeColor = Color.Red;
            errorLabel.Text = errorText;
            return false;
        }
    }
}
