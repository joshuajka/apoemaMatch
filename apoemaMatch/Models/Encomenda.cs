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

        public string Titulo { get; set; }

        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; }

        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }

        public string Descricao { get; set; }
        
       //TODO(Fazer os enuns de status)
        //Iniciado -> Demandante criou a encomenda,
       //Recusado -> Agenciador recusou a encomenda,
       //Publico -> Encomenda disponível para os solucionaores,
       //Finalizado -> encomenda deixou de ser disponível

        public bool RealizaProcessoSeletivo { get; set; }

        public List<Questao> Questoes { get; set; }
    }
}
