using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWithDB.ConnectionString
{
    internal class SQLPanel : MainPanel
    {
        public SQLPanel()
        {
            ComboBox comboBox = new ComboBox();
            Controls.Add(comboBox);

            button=new Button();
            button.Location = new Point(40, 10);
            button.Text = "Выберите файл";
            Controls.Add(button);


            ConStr = "dsaf";
        }
    }
}
