using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apoemaMatch.Data.Enums;

namespace apoemaMatch.Data.ViewModels
{
    public class EncomendaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Título da requisição")]
        [Required(ErrorMessage = "O Título da requisição é obrigatório")]
        public string Titulo { get; set; }

        [Display(Name = "Qual serviço procura?")]
        [Required(ErrorMessage = "O tipo de serviço procurado é obrigatorio")]
        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; }

        [Display(Name = "Área de solução buscada?")]
        [Required(ErrorMessage = "A área de solução buscada é obrigatorio")]
        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }

        [Display(Name = "Descreva sua encomenda")]
        [Required(ErrorMessage = "A descrição da demanda é obrigatorio")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "É necessário informar se realiza processo seletivo")]
        public bool RealizaProcessoSeletivo { get; set; }

        public EnumTipoResposta TipoResposta { get; set; }

        public List<QuestaoViewModel> Questoes { get; set; }

    }
}