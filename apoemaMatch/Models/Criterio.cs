using apoemaMatch.Data.Base;
using apoemaMatch.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Models
{
    public class Criterio : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Descricao { get; set; }
        
        public EnumTipoCriterio TipoCriterio { get; set; }

        public List<string> OpcoesCriterios { get; set; }

        public int Ordem { get; set; }

        public int ChamadaId { get; set; }

        public Chamada Chamada { get; set; }
    }
}