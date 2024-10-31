using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Interfaces
{
    internal interface IDataProcessor
    {
        User user (IDataProvider provider);
    }
}
