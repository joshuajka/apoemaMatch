using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data
{
    public enum EnumSegmentoDeMercado
    {
        [Display(Name = "Alimentos e bebidas")]
        AlimentosEBebidas = 1,
        [Display(Name = "Vestuário e calçados")]
        VestuarioECalcados = 2,
        [Display(Name = "Construção")]
        Construcao = 3,
        [Display(Name = "Saúde")]
        Saude = 4,
        [Display(Name = "Educação")]
        Educacao = 5,
        [Display(Name = "Serviços Pessoais")]
        ServicosPessoais = 6,
        [Display(Name = "Serviços Especializados")]
        ServicosEspecializados = 7,
        [Display(Name = "Informática TI")]
        InformaticaTI = 8,
        [Display(Name = "Entretenimento")]
        Entretenimento = 9,
        [Display(Name = "Financeiro")]
        Financeiro = 10,
        [Display(Name = "Outros")]
        Outros = 11
    }
}
