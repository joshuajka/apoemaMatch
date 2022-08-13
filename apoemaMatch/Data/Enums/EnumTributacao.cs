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
        LucroReal,
        [Display(Name = "Lucro Presumido")]
        LucroPresumido,
        [Display(Name = "Simples Nacional")]
        SimplesNacional
    }
}
