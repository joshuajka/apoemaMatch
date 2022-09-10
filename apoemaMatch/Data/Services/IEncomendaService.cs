using apoemaMatch.Data.Base;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public interface IEncomendaService : IEntityBaseRepository<Encomenda> 
    {
        Task<EncomendaDropDownViewModel> GetSolucionadoresDropDown(Encomenda encomenda);

        Task VincularEncomendaAsync(EncomendaViewModel encomenda);

        Task AceitarRecusarEncomendaAsync(Encomenda encomenda);

        Task<List<Encomenda>> GetAllEncomendasAsync();

        Task<Encomenda> GetEncomendaAsync(Encomenda encomenda);

        Task InsereProposta(Proposta proposta);

        Task<Encomenda> GetEncomendaByProposta(Proposta proposta);

        Task UpdateNotasRespostasCriteriosProposta(Proposta proposta);
        
        Task<Encomenda> GetEncomendaByChamada(int Id);
        
        Task AtualizaEncomendaAsync(Encomenda encomenda);

        Task<List<Proposta>> GetPropostasByEncomenda(int id);

        Task<bool> CheckDateExpiration(int id);

        Task ExcluirProposta(int Id);
    }
}
