using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apoemaMatch.Data.Enums;
using apoemaMatch.Models;

namespace apoemaMatch.Data.ViewModels
{
    public class EncomendaViewModel
    {
        public int Id { get; set; }

        #region EncomendaBase
        
        [Display(Name ="Solucionador")]
        public int? IdSolucionador { get; set; }

        public int IdDemandante { get; set; }

        public bool EncomendaAberta { get; set; }

        [Display(Name = "Título da encomenda")]
        public string Titulo { get; set; }
        
        [Display(Name = "Tipo de encomenda")]
        public EnumTipoEncomenda TipoEncomenda { get; set; }

        [Display(Name = "Descreva o tipo da encomenda")]
        public string TipoEncomendaOutros { get; set; }

        [Display(Name = "Descreva sua encomenda")]
        public string Descricao { get; set; }

        public bool PossuiChamada { get; set; }
        
        public EnumStatusEncomenda StatusEncomenda { get; set; }
        
        public string JustificativaRecusa { get; set; }

        #endregion

        #region Chamada

        public int ChamadaId { get; set; }

        public string NumeroChamada { get; set; }

        #region Detalhes
        [Display(Name = "Descreva os detalhes da seleção")]
        public string DescricaoChamada { get; set; }

        [Display(Name = "Data de validade da chamada")]
        public string DataValidadeChamada { get; set; }

        [Display(Name = "Link de Arquivo")]
        public string ArquivoDetalheChamada { get; set; }
        #endregion

        #region Criterios
        public CriterioViewModel CriterioBase { get; set; }

        public string InputCriterios { get; set; }

        public List<Criterio> Criterios { get; set; }
        #endregion

        #region Proposta

        public Proposta Proposta { get; set; }

        public List<Proposta> Propostas { get; set; }

        public bool SolucionadorLogadoPossuiPropostaNaEncomenda { get; set; }

        public bool SolucionadorLogadoEstaDisponivel { get; set; }

        #endregion

        #endregion

        public string AvaliacaoSolucionarSelecionadoChamada { get; set; }

        public int NotaSolucionarSelecionadoChamada { get; set; }
    }
}