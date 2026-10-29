using System.Drawing;
using System.Windows.Forms;

namespace UserData.UserForms.Elements
{
    internal class Checks
    {
        /// <summary>
        /// Проверяет заполнили ли TextBox.
        /// </summary>
        public static bool TextBoxComplete(TextBox textBox, string watermarkText)
        {
            if (textBox.Text != watermarkText &&
                textBox.Text != "")
                return true;
            
                textBox.ForeColor = Color.Red;
                return false;
        }
    }
}
