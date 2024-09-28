using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDB
{
    public class WorkWithDB
    {
        /// <summary>
        /// Создает строку подключения к базе данных.
        /// </summary>
        /// <returns></returns>
        public static string ConnectionStringCreate()
        {
            ConnectionString.ConStrCreate conStrCreate = new ConnectionString.ConStrCreate();
            return conStrCreate.ConStr;
        }

    }
}
