using apoemaMatch.Data.Enums;

namespace apoemaMatch.Data.ViewModels
{
    public class QuestaoViewModel
    {
        public int Id { get; set; }
        
        public string Pergunta { get; set; }
        
        public EnumTipoResposta TipoResposta { get; set; }
    }
}