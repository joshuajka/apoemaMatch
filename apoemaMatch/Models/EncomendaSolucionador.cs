using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Models
{
    public class EncomendaSolucionador
    {
        public int EncomendaId { get; set; }
        public Encomenda Encomenda { get; set; }

        public int SolucionadorId { get; set; }
        public Solucionador Solucionador { get; set; }
    }
}
