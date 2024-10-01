using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WorkWithUser.UserForms
{
    internal class MainForm : Form
    {
        MainPanel mainPanel;
        public MainForm()
        {
            Size = new Size(400, 300);
            StartPosition = FormStartPosition.CenterParent;
            AutoSize = false;
            MinimizeBox = false;
            MaximizeBox = false;

            mainPanel = new LogInPanel(this);
            Controls.Add(mainPanel);
        }
    }
}
