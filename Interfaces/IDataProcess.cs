using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces
{
    internal interface IDataProcess
    {
        IDataProvider DataProvider {  get; }

        bool TableCheck(string tableName);

    }
}
