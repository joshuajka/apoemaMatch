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
        [Display(Name = "Título da encomenda")]
        [Display(Name ="Solucionador")]
        public int? IdSolucionador { get; set; }

        public int IdDemandante { get; set; }

        public bool EncomendaAberta { get; set; }

        [Display(Name = "Título da requisição")]
        [Required(ErrorMessage = "O Título da requisição é obrigatório")]
        public string Titulo { get; set; }
        
        [Display(Name = "Tipo de encomenda")]
        [Required(ErrorMessage = "O tipo de encomenda é obrigatorio")]
        public EnumTipoEncomenda TipoEncomenda { get; set; }

        [Display(Name = "Descreva a tipo da encomenda")]
        public string TipoEncomendaOutros { get; set; }

        [Display(Name = "Descreva sua encomenda")]
        [Required(ErrorMessage = "A descrição da demanda é obrigatorio")]
        public string Descricao { get; set; }

        public bool PossuiChamada { get; set; }
        
        public EnumStatusEncomenda StatusEncomenda { get; set; }

        #endregion

        #region Chamada

        public int ChamadaId { get; set; }

        public int NumeroChamada { get; set; }

        #region Detalhes
        [Display(Name = "Descreva os detalhes da seleção")]
        public string DescricaoChamada { get; set; }

        [Display(Name = "Data de validade da seleção")]
        public string DataValidadeChamada { get; set; }

        public string ArquivoDetalheChamada { get; set; }
        #endregion

        #region Criterios
        public CriterioViewModel CriterioBase { get; set; }

        public string InputCriterios { get; set; }

        public List<Criterio> Criterios { get; set; }
        #endregion

        #endregion

    }
}