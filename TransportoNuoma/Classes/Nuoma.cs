using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportoNuoma.Classes
{
    class Nuoma
    {
        public int nuomos_Nr { get; set; }
        public DateTime nuomosPrData { get; set; }
        public DateTime nuomosPabData { get; set; }
        public TimeSpan nuomosPradLaik { get; set; }
        public TimeSpan nuomosPabLaik { get; set; }
        public int transporto_Id { get; set; }
        public int kliento_Nr { get; set; }
        public int rezId { get; set; }
    }
}
