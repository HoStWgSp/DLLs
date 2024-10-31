using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserData.Interfaces;

namespace UserData
{
    internal class DataProcessor : IDataProcessor
    {
        public User user(IDataProvider provider)
        {
            List<User> users = provider.GetUsersList();

            return new User();
        }
    }
}
