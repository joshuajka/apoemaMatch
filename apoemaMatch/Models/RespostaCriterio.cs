using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apoemaMatch.Models;
using Microsoft.JSInterop.Infrastructure;

namespace apoemaMatch.Models
{
    public class RespostaCriterio
    {
        [Key]
        public int Id { get; set; }
        
        public int CriterioId { get; set; }
        
        public Criterio Criterio { get; set; }
        
        public string RespostaUpload { get; set; }
        
        public string RespostaTextual { get; set; }
        
        public List<string> OpcoesSelecionadas { get; set; }
        
        public int Nota { get; set; }

        public int PropostaId { get; set; }

        public Proposta Proposta { get; set; }
    }
}