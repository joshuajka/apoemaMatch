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

        public async Task AdicionarDemandaAsync(DemandaViewModel demanda)
        {
            var novaDemanda = new Demanda()
            {
                DemandaAberta = true,
                ImagemURL = demanda.ImagemURL,
                Email = demanda.Email,
                NomeDemandante = demanda.NomeDemandante,
                Telefone = demanda.Telefone,
                NomeEmpresa = demanda.NomeEmpresa,
                CargoDemandante = demanda.CargoDemandante,
                TempoDeMercado = demanda.TempoDeMercado,
                PorteDaEmpresa = demanda.PorteDaEmpresa,
                RamoDeAtuacao = demanda.RamoDeAtuacao,
                SegmentoDeMercado = demanda.SegmentoDeMercado,
                LinhaDeAtuacaoTI = demanda.LinhaDeAtuacaoTI,
                RegimeDeTributacao = demanda.RegimeDeTributacao,
                LeiDeInformatica = demanda.LeiDeInformatica,
                ObjetivoParceria = demanda.ObjetivoParceria,
                AreaSolucaoBuscada = demanda.AreaSolucaoBuscada,
                Descricao = demanda.Descricao
            };
            await _context.Demandas.AddAsync(novaDemanda);
            await _context.SaveChangesAsync();

            //Adicionar Demanda Solucionador
            //foreach (var solucionadorId in demanda.SolucionadorIds)
            //{
            //    var novaDemandaSolucionador = new DemandaSolucionador()
            //    {
            //        DemandaId = novaDemanda.Id,
            //        SolucionadorId = solucionadorId
            //    };
            //    await _context.DemandasSolucionadores.AddAsync(novaDemandaSolucionador);
            //}
            //await _context.SaveChangesAsync();

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
