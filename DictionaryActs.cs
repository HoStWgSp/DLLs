using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData
{
    internal class DictionaryActs
    {
        /// <summary>
        /// Читает все значения из словаря и записыват в массив строк
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        internal static string[] ArrayFillFromDictionary(Dictionary<int, string> dictionary)
        {
            // создаем массив строк с количеством пар в словаре
            string[] strings = new string[dictionary.Count];

            // Читаем все значения в словаре и записываем в массив строк
            int i = 0;
            foreach(var item in dictionary)
            {
                strings[i] = item.Value;
                i++;
            }

            return  strings;
        }
    }
}
