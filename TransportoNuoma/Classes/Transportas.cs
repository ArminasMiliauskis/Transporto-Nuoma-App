using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportoNuoma.Classes
{
    class Transportas
    {
        public int transporto_Id { get; set; }
        public string transporto_Nr { get; set; }
        public string tipas { get; set; }
        public string spalva { get; set; }
        public DateTime gamybos_Metai { get; set; }
        public int kaina { get; set; }
        public string QRCode { get; set; }
        public int markes_Id { get; set; }

    }
}
