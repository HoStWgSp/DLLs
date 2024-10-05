using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWithUser.UserForms
{
    internal class MainPanel : Panel
    {
        //public Image UserImage {  get; set; }
        //public Button loginButton,
            //registrationButton;

        MainForm form;

        public string UserName { get; set; } = "";
        public string UserPassword { get; set; } = "";
        public MainPanel(MainForm form)
        {
            Dock = DockStyle.Fill;
            this.form = form;
            //BackColor = Color.Green;            
        }
    }
}
