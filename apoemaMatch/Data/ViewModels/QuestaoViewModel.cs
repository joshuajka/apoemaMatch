using apoemaMatch.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class QuestaoViewModel
    {
        public int Id { get; set; }

        public string Pergunta { get; set; }
        
        [Display(Name = "Tipo Resposta")]
        public EnumTipoResposta TipoResposta { get; set; }

        public OpcaoRespostaViewModel OpcaoRespostaBase { get; set; }
    }
}