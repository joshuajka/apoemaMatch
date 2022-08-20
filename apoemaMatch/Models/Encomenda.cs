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

        public string Titulo { get; set; }

        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; }

        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }

        public string Descricao { get; set; }

        public bool RealizaProcessoSeletivo { get; set; }

        public List<Questao> Questoes { get; set; }
    }
}
