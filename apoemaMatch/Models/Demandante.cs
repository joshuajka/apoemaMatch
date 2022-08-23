using apoemaMatch.Data.Enums;
using apoemaMatch.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Models
{
    public class Demandante : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "URL Foto")]
        public string ImagemURL { get; set; }

        public string IdUsuario { get; set; }

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

        public List<Encomenda> Encomendas { get; set; }

        //Relationships
        public List<EncomendaSolucionador> EncomendaSolucionador { get; set; }

    }
}
