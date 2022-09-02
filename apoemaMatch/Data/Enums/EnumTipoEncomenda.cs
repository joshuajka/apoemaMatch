using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.Enums
{
    public enum EnumTipoEncomenda
    {
        [Display(Name = "Desenvolvimento Tecnológico")]
        DesenvolvimentoTecnologico,
        [Display(Name = "Pesquisa & Desenvolvimento")]
        PeD,
        [Display(Name = "Serviço")]
        Servico,
        Treinamento,
        Outros
    }
}