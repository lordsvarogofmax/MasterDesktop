using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDesktop.Lib.Data
{
    public class Master
    {
        public int IDMASTER { get; set; }
        public string SHOTNAMEMASTER { get; set; } // Витя
        public string FULLNAMEMASTER { get; set; } // Слонич Виктор Анатольевич
        public DateTime? BIRTHDAY { get; set; }
        public string BIRTHPLACE { get; set; }
        public string ADRESSREGISTR { get; set; }
        public string ADRESSFACT { get; set; }
        public DateTime? DATARAB { get; set; }
        public DateTime? DATAUVOLN { get; set; }
        public string NOTES { get; set; }
        public int? PRIZNUVOLN { get; set; }
        public string PRICHINAUV { get; set; }
        public string TELEFON { get; set; }
        public string FOTO { get; set; }

        public override string ToString() => $"{SHOTNAMEMASTER}";

    }
}
