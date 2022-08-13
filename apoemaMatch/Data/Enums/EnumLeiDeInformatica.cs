using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Enums
{
    public enum EnumLeiDeInformatica
    {
        [Display(Name = "Já usou")]
        Ja_Usou,
        [Display(Name = "Usa")]
        Usa,
        [Display(Name = "Não usa")]
        Nao_Usa,
        [Display(Name = "Quer usar")]
        Quer_Usar
    }
}
