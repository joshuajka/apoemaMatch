using apoemaMatch.Data.Base;
using apoemaMatch.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Models
{
    public class Encomenda : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public int IdDemandante { get; set; }

        public int IdSolucionador { get; set; }

        public string Titulo { get; set; }

        public bool EncomendaAberta { get; set; }

        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; }

        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }

        public string Descricao { get; set; }

        public EnumStatusEncomenda StatusEncomenda { get; set; }

        //TODO(Inserir um campo para o agenciador justificar a recusa)

        public bool RealizaProcessoSeletivo { get; set; }

        public List<Questao> Questoes { get; set; }
    }
}
