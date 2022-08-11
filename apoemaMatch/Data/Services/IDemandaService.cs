using apoemaMatch.Data.Base;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public interface IDemandaService:IEntityBaseRepository<Demanda>
    {
        Task<Demanda> GetDemandaByIdAsync(int Id);
        Task<DemandaDropDownViewModel> GetSolucionadoresDropDown();

        Task AdicionarDemandaAsync(DemandaViewModel demanda);

        Task UpdateDemandaAsync(DemandaViewModel demanda);

        
    }
}
