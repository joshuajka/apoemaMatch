﻿
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.Enums
{
    public enum EnumTipoResposta
    {
        Textual = 1,
        [Display(Name = "Seleção única")]
        SelecaoUnica,
        [Display(Name = "Múltipla seleção")]
        MultiplaSelecao,
    }
}