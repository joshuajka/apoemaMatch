using apoemaMatch.Data.Base;
using apoemaMatch.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Models
{
    public class Encomenda : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public EnumSegmentoDeMercado AreaServico { get; set; }

        public EnumAreaSolucaoBuscada ServicoBuscado { get; set; }

        public string Descricao { get; set; }
    }
}
