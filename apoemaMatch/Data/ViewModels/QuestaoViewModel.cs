using apoemaMatch.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class QuestaoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Critério")]
        public string Pergunta { get; set; }
        
        [Display(Name = "Tipo de critério")]
        public EnumTipoResposta TipoResposta { get; set; }

        public OpcaoRespostaViewModel OpcaoRespostaBase { get; set; }
    }
}