using System.Windows.Forms;
using System.Drawing;

namespace UserData.UserForms
{
    internal class UserListForm : Form
    {
        internal UsersList usersList;
        public UserListForm(Icon formIcon)
        {
            Icon = formIcon;
            StartPosition = FormStartPosition.CenterParent;
            AutoSize = false;
            MinimizeBox = false;
            MaximizeBox = false;
        }
    }
}
