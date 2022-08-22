
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.Enums
{
    public enum EnumTipoResposta
    {
        [Display(Name = "Resposta Curta")]
        RespostaCurta,
        [Display(Name = "Parágrafo")]
        Paragrafo,
        [Display(Name = "Sim ou Não")]
        SimNao,
        [Display(Name = "Caixas de seleção")]
        CaixaSelecao,
    }
}