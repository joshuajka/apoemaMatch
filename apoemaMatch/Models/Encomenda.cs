using apoemaMatch.Data.Enums;
using apoemaMatch.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Models
{
    public class Encomenda:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Titulo da reqisição")]
        public string Titulo { get; set; }

        [Display(Name = "Qual serviço procura?")]
        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; } 
        
        [Display(Name = "Área de solução buscada")]
        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }
        
        public string Descricao { get; set; }
        
        public bool RealizaProcessoSeletivo { get; set; }
        
    }
}