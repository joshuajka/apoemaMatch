using apoemaMatch.Data.Base;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public class DemandaService : EntityBaseRepository<Demanda>, IDemandaService
    {
        private readonly AppDbContext _context;

        public DemandaService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Demanda> GetDemandaByIdAsync(int Id)
        {
            var demandaDetalhes = await _context.Demandas.Include(dm => dm.DemandaSolucionador).ThenInclude(s => s.Solucionador)
                .FirstOrDefaultAsync(n => n.Id == Id);

            return demandaDetalhes;

        }

        public async Task<DemandaDropDownViewModel> GetSolucionadoresDropDown()
        {
            var response = new DemandaDropDownViewModel()
            {
                Solucionadores = await _context.Solucionadores.OrderBy(n => n.Nome).ToListAsync()
            };
            return response;
        }
                
        
    }
}
