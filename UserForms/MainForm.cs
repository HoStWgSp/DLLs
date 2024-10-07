using System.Windows.Forms;
using System.Drawing;

namespace WorkWithUser.UserForms
{
    public class MainForm : Form
    {
        public MainPanel mainPanel;

        public MainForm(Icon formIcon)
        {
            Icon = formIcon;
            Size = new Size(400, 254);
            StartPosition = FormStartPosition.CenterParent;
            AutoSize = false;
            MinimizeBox = false;
            MaximizeBox = false;
        }
    }
}
