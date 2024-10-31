using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    public interface IDataProcess
    {
        bool CheckTable(string tableName, IDataProvider provider);
    }
}
