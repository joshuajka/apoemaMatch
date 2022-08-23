using apoemaMatch.Data.Base;
using apoemaMatch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Services
{
    public class DemandanteService : EntityBaseRepository<Demandante>, IDemandanteService
    {
        private readonly AppDbContext _context;

        public DemandanteService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AdicionarDemandanteAsync(DemandanteViewModel demandante)
        {
            var novoDemandante = new Demandante()
            {
                ImagemURL = demandante.ImagemURL,
                Email = demandante.Email,
                NomeDemandante = demandante.NomeDemandante,
                Telefone = demandante.Telefone,
                NomeEmpresa = demandante.NomeEmpresa,
                CargoDemandante = demandante.CargoDemandante,
                TempoDeMercado = demandante.TempoDeMercado,
                PorteDaEmpresa = demandante.PorteDaEmpresa,
                RamoDeAtuacao = demandante.RamoDeAtuacao,
                SegmentoDeMercado = demandante.SegmentoDeMercado,
                LinhaDeAtuacaoTI = demandante.LinhaDeAtuacaoTI,
                RegimeDeTributacao = demandante.RegimeDeTributacao,
                LeiDeInformatica = demandante.LeiDeInformatica,
                ObjetivoParceria = demandante.ObjetivoParceria,
                AreaSolucaoBuscada = demandante.AreaSolucaoBuscada,
                Descricao = demandante.Descricao
            };
            await _context.Demandantes.AddAsync(novoDemandante);
            await _context.SaveChangesAsync();
        }

        public Task<Demandante> GetDemandanteByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Demandante> GetDemandanteByIdUser(string IdUser)
        {
            var demandanteDetalhes = await _context.Demandantes.Include(dm => dm.EncomendaSolucionador).ThenInclude(s => s.Solucionador)
                .FirstOrDefaultAsync(n => n.IdUsuario == IdUser);

            return demandanteDetalhes;
        }

        //Adicionar Demandante Solucionador
        //foreach (var solucionadorId in demandante.SolucionadorIds)
        //{
        //    var novoDemandanteSolucionador = new DemandanteSolucionador()
        //    {
        //        DemandanteId = novoDemandante.Id,
        //        SolucionadorId = solucionadorId
        //    };
        //    await _context.DemandantesSolucionadores.AddAsync(novoDemandanteSolucionador);
        //}
        //await _context.SaveChangesAsync();

        //}

        /*public Task<IEnumerable<Demandante>> GetAllAsync(params Expression<Func<Demandante, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<Demandante> GetDemandanteByIdAsync(int Id)
        {
            var demandanteDetalhes = await _context.Demandantes.Include(dm => dm.DemandanteSolucionador).ThenInclude(s => s.Solucionador)
                .FirstOrDefaultAsync(n => n.Id == Id);

            return demandanteDetalhes;

        }

        public async Task<DemandanteDropDownViewModel> GetSolucionadoresDropDown(Demandante demandante)
        {
            var response = new DemandanteDropDownViewModel()
            {
                Solucionadores = await _context.Solucionadores.OrderBy(n => n.Nome).ToListAsync()

                //Solucionadores = await _context.Solucionadores.Where(n => n.AreaDePesquisa.Equals(demandante.AreaSolucaoBuscada)).OrderBy(n => n.Nome).ToListAsync()
            };
            return response;
        }*/

        public async Task UpdateAsync(int id, Demandante entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateDemandanteAsync(DemandanteViewModel demandante)
        {
            var dbDemandante = await _context.Demandantes.FirstOrDefaultAsync(n => n.Id == demandante.Id);

            if (dbDemandante != null)
            {
                dbDemandante.ImagemURL = demandante.ImagemURL;
                dbDemandante.Email = demandante.Email;
                dbDemandante.NomeDemandante = demandante.NomeDemandante;
                dbDemandante.Telefone = demandante.Telefone;
                dbDemandante.NomeEmpresa = demandante.NomeEmpresa;
                dbDemandante.CargoDemandante = demandante.CargoDemandante;
                dbDemandante.TempoDeMercado = demandante.TempoDeMercado;
                dbDemandante.PorteDaEmpresa = demandante.PorteDaEmpresa;
                dbDemandante.RamoDeAtuacao = demandante.RamoDeAtuacao;
                dbDemandante.SegmentoDeMercado = demandante.SegmentoDeMercado;
                dbDemandante.LinhaDeAtuacaoTI = demandante.LinhaDeAtuacaoTI;
                dbDemandante.RegimeDeTributacao = demandante.RegimeDeTributacao;
                dbDemandante.LeiDeInformatica = demandante.LeiDeInformatica;
                dbDemandante.ObjetivoParceria = demandante.ObjetivoParceria;
                dbDemandante.AreaSolucaoBuscada = demandante.AreaSolucaoBuscada;
                dbDemandante.Descricao = demandante.Descricao;
                await _context.SaveChangesAsync();
            }


        }

        /*public async Task VincularDemandanteAsync(DemandanteViewModel demandante)
        {
            var dbDemandante = await _context.Demandantes.FirstOrDefaultAsync(n => n.Id == demandante.Id);

            if (dbDemandante != null)
            {
                dbDemandante.ImagemURL = demandante.ImagemURL;
                dbDemandante.Email = demandante.Email;
                dbDemandante.NomeDemandante = demandante.NomeDemandante;
                dbDemandante.Telefone = demandante.Telefone;
                dbDemandante.NomeEmpresa = demandante.NomeEmpresa;
                dbDemandante.CargoDemandante = demandante.CargoDemandante;
                dbDemandante.TempoDeMercado = demandante.TempoDeMercado;
                dbDemandante.PorteDaEmpresa = demandante.PorteDaEmpresa;
                dbDemandante.RamoDeAtuacao = demandante.RamoDeAtuacao;
                dbDemandante.SegmentoDeMercado = demandante.SegmentoDeMercado;
                dbDemandante.LinhaDeAtuacaoTI = demandante.LinhaDeAtuacaoTI;
                dbDemandante.RegimeDeTributacao = demandante.RegimeDeTributacao;
                dbDemandante.LeiDeInformatica = demandante.LeiDeInformatica;
                dbDemandante.ObjetivoParceria = demandante.ObjetivoParceria;
                dbDemandante.AreaSolucaoBuscada = demandante.AreaSolucaoBuscada;
                dbDemandante.Descricao = demandante.Descricao;
                await _context.SaveChangesAsync();

                var SolucionadoresVinculados = await _context.EncomendasSolucionadores.Where(n => n.DemandanteId == dbDemandante.Id).ToListAsync();
                //var lista = SolucionadoresVinculados.Select(n=>n.SolucionadorId).ToList();
                var excecao = demandante.EncomendaSolucionadorIds.Where(p => SolucionadoresVinculados.All(p2 => p2.SolucionadorId != p)).ToList();


                foreach (var solucionadorId in excecao)
                {

                    var novoEncomendaSolucionador = new EncomendaSolucionador()
                    {
                        EncomendaId = dbDemandante.Id,
                        SolucionadorId = solucionadorId
                    };

                    await _context.EncomendasSolucionadores.AddAsync(novoEncomendaSolucionador);
                }
                await _context.SaveChangesAsync();
            }
        }*/
    }
}

