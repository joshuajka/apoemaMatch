using System;
using apoemaMatch.Data.Base;
using apoemaMatch.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Models
{
    public class Encomenda : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        
        public int DemandanteId { get; set; }
        
        public Demandante Demandante { get; set; }

        public int IdDemandante { get; set; }

        public int? IdSolucionador { get; set; }

        public string Titulo { get; set; }

        public EnumTipoEncomenda TipoEncomenda { get; set; }

        public string Descricao { get; set; }

        public EnumStatusEncomenda StatusEncomenda { get; set; }

        public string JustificativaRecusa { get; set; }

        public bool PossuiChamada { get; set; }

        public Chamada Chamada { get; set; }
        
        public DateTime DataCadastro { get; set; }
        
        public List<Questao> Questoes { get; set; }
    }
}
