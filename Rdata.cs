using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JcService
{
    public class Rdata
    {
        public int code { get; set; }
        public string msg { get; set; }
        public List<long> data { get; set; }
        public List<string> urlList { get; set; }
    }
}
