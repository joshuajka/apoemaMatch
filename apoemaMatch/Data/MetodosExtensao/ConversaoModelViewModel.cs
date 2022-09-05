using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;

namespace apoemaMatch.Data.MetodosExtensao
{
    public static class ConversaoModelViewModel
    {
        public static EncomendaViewModel Converta(this Encomenda encomenda)
        {
            return new()
            {
                EncomendaAberta = encomenda.EncomendaAberta,
                Id = encomenda.Id,
                Titulo = encomenda.Titulo,
                TipoEncomenda = encomenda.TipoEncomenda,
                Descricao = encomenda.Descricao,
                PossuiChamada = encomenda.PossuiChamada,
                StatusEncomenda = encomenda.StatusEncomenda,
                ChamadaId = encomenda.Chamada?.Id ?? 0,
                Criterios = encomenda.Chamada?.Criterios,
                Propostas = encomenda.Chamada?.Propostas,
                NumeroChamada = encomenda.Chamada?.NumeroChamada,
            };
        }
    }
}
