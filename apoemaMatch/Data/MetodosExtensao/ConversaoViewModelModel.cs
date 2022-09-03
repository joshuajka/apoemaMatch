using apoemaMatch.Data.Enums;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;

namespace apoemaMatch.Data.MetodosExtensao
{
    public static class ConversaoViewModelModel
    {
        public static Encomenda Converta(this EncomendaViewModel encomendaViewModel, EnumStatusEncomenda statusEncomenda = EnumStatusEncomenda.Inicial)
        {
            return new()
            {
                Id = encomendaViewModel.Id,
                Titulo = encomendaViewModel.Titulo,
                TipoEncomenda = encomendaViewModel.TipoEncomenda,
                Descricao = encomendaViewModel.Descricao,
                StatusEncomenda = statusEncomenda,
                PossuiChamada = encomendaViewModel.PossuiChamada,
                //TODO(Chamada)
               // Questoes = encomendaViewModel.RealizaProcessoSeletivo ? encomendaViewModel.Questoes : null
            };
        }

        public static Criterio Converta(this CriterioViewModel questaoViewModel)
        {
            return new()
            {
                Id = questaoViewModel.Id,
                Descricao = questaoViewModel.Descricao,
                TipoCriterio = questaoViewModel.TipoCriterio,
                Ordem = questaoViewModel.Ordem
            };
        }
    }
}
