using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWithUser.UserForms
{
    public class MainPanel : Panel
    {
        public bool UserAuthorized { get; set; } = false;
        public MainPanel(User user)
        {
            Dock = DockStyle.Fill;
        }
    }
}
