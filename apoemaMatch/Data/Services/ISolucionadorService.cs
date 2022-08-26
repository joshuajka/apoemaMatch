using apoemaMatch.Data.Base;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public interface ISolucionadorService:IEntityBaseRepository<Solucionador>
    {
        Task<Solucionador> GetSolucionadorByIdUser(string IdUser);

        Task UpdateSolucionadorAsync(SolucionadorViewModel solucionador);
    }
}
