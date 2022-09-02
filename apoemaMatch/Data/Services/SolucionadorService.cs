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
            var solucionadorDetalhes = await _context.Solucionadores.FirstOrDefaultAsync(n => n.IdUsuario == IdUser);

            return solucionadorDetalhes;
        }

        public async Task UpdateSolucionadorAsync(SolucionadorViewModel solucionador)
        {
            var dbSolucionador = await _context.Solucionadores.FirstOrDefaultAsync(n => n.Id == solucionador.Id);

            if (dbSolucionador != null)
            {
                dbSolucionador.ImagemURL = solucionador.ImagemURL;
                dbSolucionador.Cpf = solucionador.Cpf;
                dbSolucionador.Nome = solucionador.Nome;
                dbSolucionador.Email = solucionador.Email;
                dbSolucionador.Telefone = solucionador.Telefone;
                dbSolucionador.MiniBio = solucionador.MiniBio;
                dbSolucionador.Formacao = solucionador.Formacao;
                dbSolucionador.AreaDePesquisa = solucionador.AreaDePesquisa;
                dbSolucionador.CurriculoLattes = solucionador.CurriculoLattes;
                await _context.SaveChangesAsync();
            }
        }
    }
}
