using apoemaMatch.Data.Enums;
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
                SegmentoDeMercado = encomenda.SegmentoDeMercado,
                AreaSolucaoBuscada = encomenda.AreaSolucaoBuscada,
                Descricao = encomenda.Descricao,
                RealizaProcessoSeletivo = encomenda.RealizaProcessoSeletivo,
                TipoResposta = EnumTipoResposta.RespostaCurta,
                Questoes = encomenda.Questoes?.ConvertAll(q => q.Converta())
            };
        }

        public static QuestaoViewModel Converta(this Questao questao)
        {
            return new()
            {
                Id = questao.Id,
                Pergunta = questao.Pergunta,
                TipoResposta = questao.TipoResposta
            };
        }
    }
}
