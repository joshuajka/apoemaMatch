using apoemaMatch.Data.Enums;
using apoemaMatch.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Models
{
    public class QuestionarioSelecao:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        
        public string Pergunta { get; set; }
        
        public EnumTipoRespostaQuestionario tipoResposta { get; set; } 
        
    }
}