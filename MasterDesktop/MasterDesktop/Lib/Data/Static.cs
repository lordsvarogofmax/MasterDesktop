using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDesktop.Lib.Data
{
    public class Static
    {
        public int id { get; set; }
        public string idUser { get; set; }
        public string NameTable { get; set; }
        public int? IdTable { get; set; }
        public string Operator { get; set; }
        public string Success { get; set; }
        public string DateUpdate { get; set; }
        public string Text { get; set; }
        public string Error { get; set; }
        public string Write { get; set; }

        public override string ToString() => $"{Success}:{NameTable}:{IdTable}:{Operator}:{Text}";
        
    }
}
