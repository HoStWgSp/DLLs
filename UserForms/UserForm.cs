using System.Windows.Forms;
using System.Drawing;

namespace WorkWithUser.UserForms
{
    public class UserForm : Form
    {
        internal UserMainPanel mainPanel;
        public UserForm(Icon formIcon)
        {
            Icon = formIcon;
            StartPosition = FormStartPosition.CenterParent;
            AutoSize = false;
            MinimizeBox = false;
            MaximizeBox = false;
        }
    }
}
