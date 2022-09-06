using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using apoemaMatch.Data.Enums;

namespace apoemaMatch.Models
{
    public class Proposta
    {
        [Key]
        public int Id { get; set; }
        
        public int ChamadaId { get; set; }
        
        public Chamada Chamada { get; set; }
        
        public EnumStatusProposta StatusProposta { get; set; }
        
        public int SolucionadorId { get; set; }

        public Solucionador Solucionador { get; set; }
        
        public List<RespostaCriterio> RespostasCriterios { get; set; }

        public int Pontuacao => RespostasCriterios?.Select(r => r.Nota).Sum() ?? 0;

    }
}