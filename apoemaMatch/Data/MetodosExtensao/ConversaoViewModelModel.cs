using apoemaMatch.Data.Enums;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using System;

namespace apoemaMatch.Data.MetodosExtensao
{
    public static class ConversaoViewModelModel
    {
        public static Encomenda Converta(this EncomendaViewModel encomendaViewModel, EnumStatusEncomenda statusEncomenda = EnumStatusEncomenda.Inicial)
        {
            return new()
            {
                IdDemandante = encomendaViewModel.IdDemandante,
                EncomendaAberta = encomendaViewModel.EncomendaAberta,
                Titulo = encomendaViewModel.Titulo,
                TipoEncomenda = encomendaViewModel.TipoEncomenda,
                Descricao = encomendaViewModel.Descricao,
                StatusEncomenda = statusEncomenda,
                PossuiChamada = encomendaViewModel.PossuiChamada,
                Chamada = !encomendaViewModel.PossuiChamada ?
                    null
                    : new()
                    {
                        Id = encomendaViewModel.ChamadaId,
                        DescricaoChamada = encomendaViewModel.DescricaoChamada,
                        DataValidade = DateTime.Parse(encomendaViewModel.DataValidadeChamada),
                        ArquivoAnexo = encomendaViewModel.ArquivoDetalheChamada,
                        Criterios = encomendaViewModel.Criterios,
                        EncomendaId = encomendaViewModel.Id
                    }
                //TODO(Chamada)
               // Questoes = encomendaViewModel.RealizaProcessoSeletivo ? encomendaViewModel.Questoes : null
            };
        }
    }
}
