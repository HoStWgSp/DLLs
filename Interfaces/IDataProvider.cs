using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    public interface IDataProvider
    {
        bool Connection { get; set; }
        bool TableCheck(string tableName);
        bool RequestToDB(string requestString);
    }
}
