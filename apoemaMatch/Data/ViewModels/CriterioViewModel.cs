using apoemaMatch.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class CriterioViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Critério")]
        public string Descricao { get; set; }
        
        [Display(Name = "Tipo de critério")]
        public EnumTipoCriterio TipoCriterio { get; set; }

        public List<string> OpcoesCriterioBase { get; set; } = new(2);

        public int Ordem { get; set; }
    }
}