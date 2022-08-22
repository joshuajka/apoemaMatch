using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;

namespace apoemaMatch.Data.MetodosExtensao
{
    public static class ConversaoViewModelModel
    {
        public static Encomenda Converta(this EncomendaViewModel encomendaViewModel)
        {
            return new()
            {
                Id = encomendaViewModel.Id,
                Titulo = encomendaViewModel.Titulo,
                SegmentoDeMercado = encomendaViewModel.SegmentoDeMercado,
                AreaSolucaoBuscada = encomendaViewModel.AreaSolucaoBuscada,
                Descricao = encomendaViewModel.Descricao,
                RealizaProcessoSeletivo = encomendaViewModel.RealizaProcessoSeletivo,
                Questoes = encomendaViewModel.Questoes?.ConvertAll(q => q.Converta())
            };
        }

        public static Questao Converta(this QuestaoViewModel questaoViewModel)
        {
            return new()
            {
                Id = questaoViewModel.Id,
                Pergunta = questaoViewModel.Pergunta,
                TipoResposta = questaoViewModel.TipoResposta
            };
        }
    }
}
