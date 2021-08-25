using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileArchiver.utils
{
    public class Util
    {
        public static string getRecordName(string path)
        {
            string name = "";
            string aux1 = path.Replace(@"\", "|");
            string[] aux = aux1.Split('|');
            name = aux[aux.Length - 1];
            return name;
        }
    }
}
