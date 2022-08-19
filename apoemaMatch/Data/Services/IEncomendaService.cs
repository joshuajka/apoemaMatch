using apoemaMatch.Data.Base;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public interface IEncomendaService:IEntityBaseRepository<Encomenda>
    {
        Task<Encomenda> GetEncomendaByIdAsync(int Id);

        Task AdicionarEncomendaAsync(EncomendaViewModel encomenda);

        Task UpdateEncomendaAsync(EncomendaViewModel encomenda);
        
    }
}