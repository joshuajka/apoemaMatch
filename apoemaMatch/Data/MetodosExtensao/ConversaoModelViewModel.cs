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
                RealizaProcessoSeletivo = encomenda.PossuiChamada,
               //TODO(Chamada)
                //Questoes = encomenda.Questoes
            };
        }

        public static QuestaoViewModel Converta(this Criterio criterio)
        {
            return new()
            {
                Id = criterio.Id,
                Pergunta = criterio.Descricao,
                TipoCriterio = criterio.TipoCriterio,
                Ordem = criterio.Ordem
            };
        }
    }
}
