using apoemaMatch.Models;
using System.Collections.Generic;

namespace apoemaMatch.Data.ViewModels
{
    public class EncomendaDropDownViewModel
    {
        public EncomendaDropDownViewModel()
        {
            Solucionadores = new List<Solucionador>();
        }

        public List<Solucionador> Solucionadores { get; set; }
    }
}
