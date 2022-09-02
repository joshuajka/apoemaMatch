using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

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
        
        public List<OpcaoCriterio> OpcoesSelecionadas { get; set; }
        
        public int Nota { get; set; }
        
    }
}