using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using WorkWithUser.UserForms.LogIn;

namespace WorkWithUser.UserForms
{
    internal class MainForm : Form
    {
        public Icon FormIcon { set { Icon = value; } }

        public MainPanel mainPanel;
        public MainForm(Icon formIcon)
        {
            FormIcon = formIcon;
            Size = new Size(400, 270);
            StartPosition = FormStartPosition.CenterParent;
            AutoSize = false;
            MinimizeBox = false;
            MaximizeBox = false;
        }
    }
}
