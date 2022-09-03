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
    public class EncomendaService : EntityBaseRepository<Encomenda>, IEncomendaService
    {
        private readonly AppDbContext _context;

        public EncomendaService(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<EncomendaDropDownViewModel> GetSolucionadoresDropDown(Encomenda encomenda)
        {
            var response = new EncomendaDropDownViewModel()
            {
                Solucionadores = await _context.Solucionadores.OrderBy(n => n.Nome).ToListAsync()

            };
            return response;
        }

        public async Task VincularEncomendaAsync(EncomendaViewModel encomenda)
        {
            var dbEncomenda= await _context.Encomendas.FirstOrDefaultAsync(n => n.Id == encomenda.Id);

            if (dbEncomenda != null)
            {
                dbEncomenda.PossuiChamada = encomenda.PossuiChamada;
                dbEncomenda.TipoEncomenda = encomenda.TipoEncomenda;
                dbEncomenda.Titulo = encomenda.Titulo;
                dbEncomenda.Descricao = encomenda.Descricao;
                dbEncomenda.StatusEncomenda = encomenda.StatusEncomenda;
                //TODO(Chamada)
                // dbEncomenda.Questoes = encomenda.Questoes;

                await _context.SaveChangesAsync();

                //var SolucionadoresVinculado = await _context.EncomendasSolucionadores.Where(n => n.EncomendaId == dbEncomenda.Id).ToListAsync();
                //var lista = SolucionadoresVinculados.Select(n=>n.SolucionadorId).ToList();
                //var excecao = encomenda.EncomendaSolucionadorId.Where(p => SolucionadoresVinculados.All(p2 => p2.SolucionadorId != p)).ToList();
                //var solucionadorId = SolucionadoresVinculado.Select(n => n.SolucionadorId).ToList();

                    var novoEncomendaSolucionador = new EncomendaSolucionador()
                    {
                        EncomendaId = dbEncomenda.Id,
                        SolucionadorId = encomenda.SolucionadorId
                    };

                    await _context.EncomendasSolucionadores.AddAsync(novoEncomendaSolucionador);

                await _context.SaveChangesAsync();
            }
        }

    }
}
