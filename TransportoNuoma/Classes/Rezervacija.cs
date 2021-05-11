using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportoNuoma.Classes
{
    class Rezervacija
    {
        public int rezervacijos_Id { get; set; }
        public DateTime rezervacijos_Data { get; set; }
        public TimeSpan rezervacijosPrad { get; set; }
        public TimeSpan rezervacijosPab { get; set; }
        public int kliento_Id { get; set; }
        public int Transporto_Id { get; set; }
        public int lokacijos_Id { get; set; }
    }
}
