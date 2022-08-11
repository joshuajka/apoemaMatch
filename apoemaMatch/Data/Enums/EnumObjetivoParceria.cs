using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Enums
{
    public enum EnumObjetivoParceria
    {
        [Display(Name = "Pesquisa/Desenvolvimento/Inovação")]
        Pesquisa_Desenvolvimento_Inovacao = 1 ,
        [Display(Name = "Disponibilização Transferência Tech")]
        Disponibilizacao_Transferencia_Tech,
        [Display(Name = "Capacitação técnica áreas especializadas")]
        Capacitacao_Tecnica_Areas_Especializadas,
        [Display(Name = "Formação RH PDI")]
        Formacao_RH_PDI,
        [Display(Name = "Formação RH Mestrado/Doutorado")]
        Formacao_RH_Mestrado_Doutorado,
        [Display(Name = "Formação Especialização Residência")]
        Formacao_RH_Especializacao_Residencia
    }
}
