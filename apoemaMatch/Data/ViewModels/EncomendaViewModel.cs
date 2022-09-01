﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using apoemaMatch.Data.Enums;
using apoemaMatch.Models;

namespace apoemaMatch.Data.ViewModels
{
    public class EncomendaViewModel
    {
        public int Id { get; set; }

        public bool EncomendaAberta { get; set; }

        [Display(Name = "Título da requisição")]
        [Required(ErrorMessage = "O Título da requisição é obrigatório")]
        public string Titulo { get; set; }

        [Display(Name = "Área de solução buscada?")]
        [Required(ErrorMessage = "A área de solução buscada é obrigatorio")]
        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; }
        
        [Display(Name = "Qual serviço procura?")]
        [Required(ErrorMessage = "O tipo de serviço procurado é obrigatorio")]
        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }

        [Display(Name = "Descreva sua encomenda")]
        [Required(ErrorMessage = "A descrição da demanda é obrigatorio")]
        public string Descricao { get; set; }

        [Display(Name = "Descreva os detalhes da seleção")]
        [Required(ErrorMessage = "A descrição da seleção é obrigatorio")]
        public string DescricaoChamada { get; set; }

        [Display(Name = "Data de validade da seleção")]
        [Required(ErrorMessage = "A data de validade de seleção é obrigatorio")]
        public string DataValidadeSelecao { get; set; }

        public string ArquivoDetalheSelecao { get; set; }

        public bool RealizaProcessoSeletivo { get; set; }

        public EnumStatusEncomenda StatusEncomenda { get; set; }

        public QuestaoViewModel QuestaoBase { get; set; }

        public string InputQuestoes { get; set; }

        public List<Questao> Questoes { get; set; }

        [Display(Name = "Solucionador")]
        public int SolucionadorId { get; set; }
    }
}