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
                Id = encomenda.Id,
                Titulo = encomenda.Titulo,
                TipoEncomenda = encomenda.TipoEncomenda,
                Descricao = encomenda.Descricao,
                PossuiChamada = encomenda.PossuiChamada,
               //TODO(Chamada)
                //Questoes = encomenda.Questoes
            };
        }

        public static CriterioViewModel Converta(this Criterio criterio)
        {
            return new()
            {
                Id = criterio.Id,
                Descricao = criterio.Descricao,
                TipoCriterio = criterio.TipoCriterio,
                Ordem = criterio.Ordem
            };
        }
    }
}
