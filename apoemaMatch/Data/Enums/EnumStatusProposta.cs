using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.Enums
{
    public enum EnumStatusProposta
    {
        [Display(Name = "Aguardando Avaliação")]
        AguardandoAvaliacao, //Quando a proposta foi enviada porém ainda não foi avaliada pelo demandante, com esse status a proposta poderá ser excluida ou editada
        [Display(Name = "Não Selecionado")]
        NaoSelecionado, //O demandate avaliou e porém não selecionou a proposta
        Aprovado, // A proposta foi aprovada pelo demandante
        
    }
}