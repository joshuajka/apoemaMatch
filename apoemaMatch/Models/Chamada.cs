using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Models
{
    public class Chamada
    {
        [Key]
        public int Id { get; set; }
        
        public int EncomendaId { get; set; }
        
        public Encomenda Encomenda { get; set; }
        
        public DateTime DataValidade { get; set; }
        
        public string DescricaoChamada { get; set; }
        
        public string ArquivoAnexo { get; set; } //TODO(Ver como armazenar isso)
        
        public List<Criterio> Criterios { get; set; }
        
        public List<Proposta> Propostas { get; set; }

        public string NumeroChamada => $"{Id:00}/{Encomenda.DataCadastro.Year}";
    }
}