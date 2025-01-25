using DataProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    public class Provider : IDataProcess
    {
        IDataProvider DataProvider { get; }
        public Provider(IDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public bool TableCheck(string tableName)
        {
            return DataProvider.TableCheck(tableName);
        }
    }
}
