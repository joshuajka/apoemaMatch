using apoemaMatch.Data.Base;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public interface IEncomendaService : IEntityBaseRepository<Encomenda> 
    {
        Task<EncomendaDropDownViewModel> GetSolucionadoresDropDown(Encomenda encomenda);

        Task VincularEncomendaAsync(EncomendaViewModel encomenda);

        Task AceitarRecusarEncomendaAsync(Encomenda encomenda);

        Task<Encomenda> GetEncomendaAsync(Encomenda encomenda);
    }
}
