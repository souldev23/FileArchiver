using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileArchiver
{
    public class Config
    {
        public string source { get; set; }
        public string destiny { get; set; }
        public string[] exceptions { get; set; }
        public string fIni {get; set;}
        public string fFin { get; set; }
        public bool testMode { get; set; }
     }
}
