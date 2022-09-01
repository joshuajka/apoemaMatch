﻿using apoemaMatch.Data.Enums;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;

namespace apoemaMatch.Data.MetodosExtensao
{
    public static class ConversaoViewModelModel
    {
        public static Encomenda Converta(this EncomendaViewModel encomendaViewModel, EnumStatusEncomenda statusEncomenda = EnumStatusEncomenda.Iniciado)
        {
            return new()
            {
                Id = encomendaViewModel.Id,
                Titulo = encomendaViewModel.Titulo,
                SegmentoDeMercado = encomendaViewModel.SegmentoDeMercado,
                AreaSolucaoBuscada = encomendaViewModel.AreaSolucaoBuscada,
                Descricao = encomendaViewModel.Descricao,
                StatusEncomenda = statusEncomenda,
                RealizaProcessoSeletivo = encomendaViewModel.RealizaProcessoSeletivo,
                Questoes = encomendaViewModel.RealizaProcessoSeletivo ? encomendaViewModel.Questoes : null
            };
        }

        public static Questao Converta(this QuestaoViewModel questaoViewModel)
        {
            return new()
            {
                Id = questaoViewModel.Id,
                Pergunta = questaoViewModel.Pergunta,
                TipoResposta = questaoViewModel.TipoResposta,
                Ordem = questaoViewModel.Ordem
            };
        }
    }
}
