using apoemaMatch.Data.Base;
using apoemaMatch.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Models
{
    public class Questao:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        
        public string Pergunta { get; set; }
        
        public EnumTipoResposta TipoResposta { get; set; }
    }
}