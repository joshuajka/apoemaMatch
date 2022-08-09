using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Models
{
    public class DemandaSolucionador
    {
        public int DemandaId { get; set; }
        public Demanda Demanda { get; set; }

        public int SolucionadorId { get; set; }
        public Solucionador Solucionador { get; set; }
    }
}
