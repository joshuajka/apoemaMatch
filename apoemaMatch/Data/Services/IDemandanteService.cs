using apoemaMatch.Data.Base;
using apoemaMatch.Models;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public interface IDemandanteService : IEntityBaseRepository<Demandante>
    {
        Task<Demandante> GetDemandanteByIdAsync(int Id);

        Task AdicionarDemandanteAsync(DemandanteViewModel demanda);

        Task UpdateDemandanteAsync(DemandanteViewModel demanda);

        Task<Demandante> GetDemandanteByIdUser(string IdUser);
    }
}
