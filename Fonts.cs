using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataBase
{
    public class Fonts
    {
        public static Font SearchHeaderFont() { return new Font("Calibri", 12, FontStyle.Bold); }
        public static Font SearchLabelFont() { return new Font("Calibri", 12, FontStyle.Regular); }
        public static Font SearchButtonFont() { return new Font("Calibri", 12, FontStyle.Bold); }
    }
}
