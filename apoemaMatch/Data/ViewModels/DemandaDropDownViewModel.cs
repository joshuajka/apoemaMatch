using apoemaMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.ViewModels
{
    public class DemandaDropDownViewModel
    {
        public DemandaDropDownViewModel()
        {
            Solucionadores = new List<Solucionador>();
        }

        public List<Solucionador> Solucionadores { get; set; }
    }
}
