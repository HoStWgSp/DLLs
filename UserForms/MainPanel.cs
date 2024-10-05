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
        //public Image UserImage {  get; set; }
        //public Button loginButton,
            //registrationButton;

        //User user;

        public DataRow userDataRow { get; set; }

        public string UserName { get; set; } = "";
        public string UserPassword { get; set; } = "";
        public MainPanel(User user)
        {
            Dock = DockStyle.Fill;
            //this.user = user;
        }

        //public virtual void LogInFormError(bool loginError = false, bool passwordError = false) { }
    }
}
