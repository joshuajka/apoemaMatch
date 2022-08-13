using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Enums
{
    public enum EnumSegmentoDeMercado
    {
        [Display(Name = "Alimentos e bebidas")]
        AlimentosEBebidas,
        [Display(Name = "Vestuário e calçados")]
        VestuarioECalcados,
        [Display(Name = "Construção")]
        Construcao,
        [Display(Name = "Saúde")]
        Saude,
        [Display(Name = "Educação")]
        Educacao,
        [Display(Name = "Serviços Pessoais")]
        ServicosPessoais,
        [Display(Name = "Serviços Especializados")]
        ServicosEspecializados,
        [Display(Name = "Informática TI")]
        InformaticaTI,
        [Display(Name = "Entretenimento")]
        Entretenimento,
        [Display(Name = "Financeiro")]
        Financeiro,
        [Display(Name = "Outros")]
        Outros
    }
}
