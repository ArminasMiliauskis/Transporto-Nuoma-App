using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportoNuoma.Classes
{
    class Lokacija
    {
        public int lokacijos_Id { get; set; }
        public string salis { get; set; }
        public string miestas { get; set; }
        public double koordinatesX { get; set; }
        public double koordinatesY { get; set; }
        public int transporto_Id { get; set; }
    }
}
