using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDesktop.Lib.Data
{
    public class Sostzakaz
    {
        public int SOSTZAKAZID { get; set; }
        public string SOSTZAKAZ { get; set; }
        public int? COLOR { get; set; }

        public override string ToString()
        {
            return $"{SOSTZAKAZ}";
        }
    }
}
