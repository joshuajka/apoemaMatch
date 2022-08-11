using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Enums
{
    public enum EnumTributacao
    {
        [Display(Name = "Lucro Real")]
        LucroReal = 1,
        [Display(Name = "Lucro Presumido")]
        LucroPresumido = 2,
        [Display(Name = "Simples Nacional")]
        SimplesNacional = 3
    }
}
