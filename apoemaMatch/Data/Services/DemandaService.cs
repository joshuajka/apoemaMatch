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

        public async Task<DemandaDropDownViewModel> GetSolucionadoresDropDown(Demanda demanda)
        {
            var response = new DemandaDropDownViewModel()
            {
                Solucionadores = await _context.Solucionadores.OrderBy(n => n.Nome).ToListAsync()

                //Solucionadores = await _context.Solucionadores.Where(n => n.AreaDePesquisa.Equals(demanda.AreaSolucaoBuscada)).OrderBy(n => n.Nome).ToListAsync()
            };
            return response;
        }

        public async Task UpdateDemandaAsync(DemandaViewModel demanda)
        {
            var dbDemanda = await _context.Demandas.FirstOrDefaultAsync(n => n.Id == demanda.Id);

            if(dbDemanda != null)
            {
                dbDemanda.ImagemURL = demanda.ImagemURL;
                dbDemanda.Email = demanda.Email;
                dbDemanda.NomeDemandante = demanda.NomeDemandante;
                dbDemanda.Telefone = demanda.Telefone;
                dbDemanda.NomeEmpresa = demanda.NomeEmpresa;
                dbDemanda.CargoDemandante = demanda.CargoDemandante;
                dbDemanda.TempoDeMercado = demanda.TempoDeMercado;
                dbDemanda.PorteDaEmpresa = demanda.PorteDaEmpresa;
                dbDemanda.RamoDeAtuacao = demanda.RamoDeAtuacao;
                dbDemanda.SegmentoDeMercado = demanda.SegmentoDeMercado;
                dbDemanda.LinhaDeAtuacaoTI = demanda.LinhaDeAtuacaoTI;
                dbDemanda.RegimeDeTributacao = demanda.RegimeDeTributacao;
                dbDemanda.LeiDeInformatica = demanda.LeiDeInformatica;
                dbDemanda.ObjetivoParceria = demanda.ObjetivoParceria;
                dbDemanda.AreaSolucaoBuscada = demanda.AreaSolucaoBuscada;
                dbDemanda.Descricao = demanda.Descricao;
                await _context.SaveChangesAsync();
            }


            }

        public async Task VincularDemandaAsync(DemandaViewModel demanda)
        {
            var dbDemanda = await _context.Demandas.FirstOrDefaultAsync(n => n.Id == demanda.Id);

            if (dbDemanda != null)
            {
                dbDemanda.ImagemURL = demanda.ImagemURL;
                dbDemanda.Email = demanda.Email;
                dbDemanda.NomeDemandante = demanda.NomeDemandante;
                dbDemanda.Telefone = demanda.Telefone;
                dbDemanda.NomeEmpresa = demanda.NomeEmpresa;
                dbDemanda.CargoDemandante = demanda.CargoDemandante;
                dbDemanda.TempoDeMercado = demanda.TempoDeMercado;
                dbDemanda.PorteDaEmpresa = demanda.PorteDaEmpresa;
                dbDemanda.RamoDeAtuacao = demanda.RamoDeAtuacao;
                dbDemanda.SegmentoDeMercado = demanda.SegmentoDeMercado;
                dbDemanda.LinhaDeAtuacaoTI = demanda.LinhaDeAtuacaoTI;
                dbDemanda.RegimeDeTributacao = demanda.RegimeDeTributacao;
                dbDemanda.LeiDeInformatica = demanda.LeiDeInformatica;
                dbDemanda.ObjetivoParceria = demanda.ObjetivoParceria;
                dbDemanda.AreaSolucaoBuscada = demanda.AreaSolucaoBuscada;
                dbDemanda.Descricao = demanda.Descricao;
                await _context.SaveChangesAsync();

                var SolucionadoresVinculados = await _context.DemandasSolucionadores.Where(n => n.DemandaId == dbDemanda.Id).ToListAsync();
                //var lista = SolucionadoresVinculados.Select(n=>n.SolucionadorId).ToList();
                var excecao = demanda.DemandaSolucionadorIds.Where(p => SolucionadoresVinculados.All(p2 => p2.SolucionadorId != p)).ToList();


                foreach (var solucionadorId in excecao)
                {

                    var novaDemandaSolucionador = new DemandaSolucionador()
                    {
                        DemandaId = dbDemanda.Id,
                        SolucionadorId = solucionadorId
                    };

                    await _context.DemandasSolucionadores.AddAsync(novaDemandaSolucionador);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
