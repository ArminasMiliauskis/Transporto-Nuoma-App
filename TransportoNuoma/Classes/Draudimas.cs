using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportoNuoma.Classes
{
    class Draudimas
    {
        public int draudId { get; set; }
        public DateTime draudPradData;
        public DateTime draudPabData;
        public int tiekId { get; set; }
        public int Trans_Id { get; set; }
    }
}
