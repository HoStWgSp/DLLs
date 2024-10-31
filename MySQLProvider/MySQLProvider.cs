using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.Interfaces;

namespace UserData.MySQLProvider
{
    internal class MySQLProvider : IDataProvider
    {
        public List<User> GetUsersList()
        {

            return new List<User>();
        }
    }
}
