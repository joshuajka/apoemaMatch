using apoemaMatch.Data.Base;
using apoemaMatch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public class SolucionadorService : EntityBaseRepository<Solucionador>, ISolucionadorService
    {
        private readonly AppDbContext _context;

        public SolucionadorService(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Solucionador> GetSolucionadorByIdUser(string IdUser)
        {
            //var solucionadorDetalhes = await _context.Solucionadores.FirstOrDefaultAsync(n => n.IdUsuario == IdUser);
            var solucionadorDetalhes = await _context.Solucionadores.Include(dm => dm.EncomendaSolucionador).ThenInclude(s => s.Solucionador)
                .FirstOrDefaultAsync(n => n.IdUsuario == IdUser);

            return solucionadorDetalhes;
        }
    }
}
