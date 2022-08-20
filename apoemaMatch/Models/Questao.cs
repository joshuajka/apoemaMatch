using apoemaMatch.Data.Enums;
using apoemaMatch.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apoemaMatch.Models
{
    public class Questao:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        
        public string Pergunta { get; set; }
        
        public EnumTipoResposta tipoResposta { get; set; }
        
    }
}