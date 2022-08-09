using apoemaMatch.Data;
using apoemaMatch.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Models
{
    public class Demanda:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public bool DemandaAberta { get; set; }

        [Display(Name = "Foto")]
        public string ImagemURL { get; set; }

        public string Email { get; set; }

        public string NomeDemandante { get; set; }

        public string Telefone { get; set; }

        public string NomeEmpresa { get; set; }

        public string CargoDemandante { get; set; }

        public int TempoDeMercado { get; set; }

        public EnumPorteDaEmpresa PorteDaEmpresa { get; set; } 

        public EnumRamoDeAtuacao RamoDeAtuacao { get; set; } 

        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; } 

        public EnumLinhaDeAtuacaoTI LinhaDeAtuacaoTI { get; set; } 

        public EnumTributacao RegimeDeTributacao { get; set; }

        public EnumLeiDeInformatica LeiDeInformatica { get; set; }  

        public EnumObjetivoParceria ObjetivoParceria { get; set; } 

        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }

        public string Descricao { get; set; }

        //Relationships
        public List<DemandaSolucionador> DemandaSolucionador { get; set; }

    }
}
