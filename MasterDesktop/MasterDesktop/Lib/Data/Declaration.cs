using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDesktop.Lib.Data
{
    public class Declaration
    {
        public int DECLID { get; set; }
        public int? DECLN { get; set; }
        public DateTime? DECLDATA { get; set; }
        public int? KVN { get; set; }
        public string TELEFON { get; set; }
        public string FIO { get; set; }
        public string DECLARATION { get; set; }
        public DateTime? DECLTIME { get; set; }
        public int? PODCOD { get; set; }
        public int? MASTERID { get; set; }
        public DateTime? DATAVIPOLN { get; set; }
        public int? SOSTZAKAZID { get; set; }
        public string NOTES { get; set; }
        public DateTime? ZAPLANIR { get; set; }
        public string USERNAME { get; set; }

        public override string ToString() => $"{FIO}:{TELEFON}";

    }
}
