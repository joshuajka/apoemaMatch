using apoemaMatch.Data.Base;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
                Solucionadores = await _context.Solucionadores.Where(_ => _.Disponivel == true).OrderBy(n => n.Nome).ToListAsync()
            };
            return response;
        }

        public async Task VincularEncomendaAsync(EncomendaViewModel encomenda)
        {
            var dbEncomenda = await _context.Encomendas.FirstOrDefaultAsync(n => n.Id == encomenda.Id);

            if (dbEncomenda != null)
            {
                dbEncomenda.PossuiChamada = encomenda.PossuiChamada;
                dbEncomenda.TipoEncomenda = encomenda.TipoEncomenda;
                dbEncomenda.Titulo = encomenda.Titulo;
                dbEncomenda.Descricao = encomenda.Descricao;
                dbEncomenda.StatusEncomenda = encomenda.StatusEncomenda;
                //TODO(Chamada)
                // dbEncomenda.Questoes = encomenda.Questoes;
                dbEncomenda.IdDemandante = encomenda.IdDemandante;
                dbEncomenda.IdSolucionador = encomenda.IdSolucionador;
                //dbEncomenda.EncomendaAberta = encomenda.EncomendaAberta;

                await _context.SaveChangesAsync();

                //var SolucionadoresVinculado = await _context.EncomendasSolucionadores.Where(n => n.EncomendaId == dbEncomenda.Id).ToListAsync();
                //var lista = SolucionadoresVinculados.Select(n=>n.SolucionadorId).ToList();
                //var excecao = encomenda.EncomendaSolucionadorId.Where(p => SolucionadoresVinculados.All(p2 => p2.SolucionadorId != p)).ToList();
            }
        }

        public async Task AceitarRecusarEncomendaAsync(Encomenda encomenda)
        {
            var dbEncomenda = await _context.Encomendas.FirstOrDefaultAsync(n => n.Id == encomenda.Id);

            if (dbEncomenda != null)
            {
                dbEncomenda.PossuiChamada = encomenda.PossuiChamada;
                dbEncomenda.TipoEncomenda = encomenda.TipoEncomenda;
                dbEncomenda.Titulo = encomenda.Titulo;
                dbEncomenda.Descricao = encomenda.Descricao;
                dbEncomenda.StatusEncomenda = encomenda.StatusEncomenda;
                dbEncomenda.Chamada = encomenda.Chamada;
                dbEncomenda.IdDemandante = encomenda.IdDemandante;
                dbEncomenda.IdSolucionador = encomenda.IdSolucionador;
                dbEncomenda.EncomendaAberta = encomenda.EncomendaAberta;
                dbEncomenda.EncomendaAberta = encomenda.EncomendaAberta;

                await _context.SaveChangesAsync();

                //var SolucionadoresVinculado = await _context.EncomendasSolucionadores.Where(n => n.EncomendaId == dbEncomenda.Id).ToListAsync();
                //var lista = SolucionadoresVinculados.Select(n=>n.SolucionadorId).ToList();
                //var excecao = encomenda.EncomendaSolucionadorId.Where(p => SolucionadoresVinculados.All(p2 => p2.SolucionadorId != p)).ToList();
            }
        }

        public async Task<List<Encomenda>> GetAllEncomendasAsync()
        {
            List<Encomenda> encomendas = await _context.Encomendas.ToListAsync();

            foreach(var encomenda in encomendas)
            {
                Chamada chamada = await _context.Chamada.FirstOrDefaultAsync(c => c.EncomendaId == encomenda.Id);

                if (chamada is not null)
                {
                    List<Criterio> criterios = await _context.Criterio.Where(c => c.ChamadaId == chamada.Id).ToListAsync();

                    List<Proposta> propostas = await _context.Proposta.Where(p => p.ChamadaId == chamada.Id).ToListAsync();

                    foreach (Proposta proposta in propostas)
                    {
                        proposta.RespostasCriterios = new();
                        foreach (Criterio criterio in criterios)
                        {
                            RespostaCriterio respostaCriterio =
                                await _context.RespostaCriterio
                                .FirstOrDefaultAsync(r => r.CriterioId == criterio.Id && r.PropostaId == proposta.Id);
                        }

                        var solucionador = await _context.Solucionadores.FirstOrDefaultAsync(s => s.Id == proposta.SolucionadorId);
                        proposta.Solucionador = solucionador;

                        proposta.Chamada = chamada;
                        proposta.ChamadaId = chamada.Id;
                    }

                    chamada.Criterios = criterios;
                    chamada.Propostas = propostas;
                    encomenda.Chamada = chamada;
                }
            }

            return encomendas;
        }

        public async Task<Encomenda> GetEncomendaAsync(Encomenda encomenda)
        {
            Encomenda encomendaBuscada =
                await _context.Encomendas
                              .FirstOrDefaultAsync(e => e.Id == encomenda.Id);

            if (encomendaBuscada is not null)
            {
                Chamada chamada = await _context.Chamada.FirstOrDefaultAsync(c => c.EncomendaId == encomendaBuscada.Id);

                if (chamada is not null)
                {
                    List<Criterio> criterios = await _context.Criterio.Where(c => c.ChamadaId == chamada.Id).ToListAsync();
                    
                    List<Proposta> propostas = await _context.Proposta.Where(p => p.ChamadaId == chamada.Id).ToListAsync();

                    foreach (Proposta proposta in propostas)
                    {
                        proposta.RespostasCriterios = new();
                        foreach (Criterio criterio in criterios)
                        {
                            RespostaCriterio respostaCriterio = 
                                await _context.RespostaCriterio
                                .FirstOrDefaultAsync(r => r.CriterioId == criterio.Id && r.PropostaId == proposta.Id);
                        }

                        var solucionador = await _context.Solucionadores.FirstOrDefaultAsync(s => s.Id == proposta.SolucionadorId);
                        proposta.Solucionador = solucionador;

                        proposta.Chamada = chamada;
                        proposta.ChamadaId = chamada.Id;
                    }

                    chamada.Criterios = criterios;
                    chamada.Propostas = propostas;
                    encomendaBuscada.Chamada = chamada;
                }
            }

            return encomendaBuscada;
        }

        public async Task InsereProposta(Proposta proposta)
        {
            await _context.Proposta.AddAsync(proposta);
            await _context.SaveChangesAsync();
        }

        public async Task<Encomenda> GetEncomendaByProposta(Proposta proposta)
        {
            Proposta propostaBuscada = await _context.Proposta.FirstOrDefaultAsync(p => p.Id == proposta.Id);
            var chamada = await _context.Chamada.FirstOrDefaultAsync(c => c.Id == propostaBuscada.ChamadaId);
            var criteriosChamada = await _context.Criterio.Where(c => c.ChamadaId == chamada.Id).ToListAsync();
            propostaBuscada.RespostasCriterios = new();
            foreach (Criterio criterio in criteriosChamada)
            {
                RespostaCriterio respostaCriterio =
                    await _context.RespostaCriterio
                    .FirstOrDefaultAsync(r => r.CriterioId == criterio.Id && r.PropostaId == propostaBuscada.Id);
            }
            var encomenda = await _context.Encomendas.FirstOrDefaultAsync(e => e.Id == chamada.EncomendaId);

            chamada.Criterios = criteriosChamada;
            chamada.Propostas = new()
            {
                propostaBuscada
            };
            encomenda.Chamada = chamada;

            return encomenda;
        }

        public async Task UpdateNotasRespostasCriteriosProposta(Proposta proposta)
        {
            foreach (var resposta in proposta.RespostasCriterios)
            {
                var respostaPersistida =
                    await _context.RespostaCriterio
                    .FirstOrDefaultAsync(r => r.CriterioId == resposta.CriterioId && r.PropostaId == proposta.Id);

                respostaPersistida.Nota = resposta.Nota;
            }
            await _context.SaveChangesAsync();
        }
        
        public async Task<Encomenda> GetEncomendaByChamada(int Id)
        {
            Chamada chamadaBuscada = await _context.Chamada.FirstOrDefaultAsync(p => p.Id == Id);
            var encomenda = await _context.Encomendas.FirstOrDefaultAsync(e => e.Id == chamadaBuscada.EncomendaId);
            return encomenda;
        }
        
        public async Task AtualizaEncomendaAsync(Encomenda encomenda)
        {
            var dbEncomenda = await _context.Encomendas.FirstOrDefaultAsync(n => n.Id == encomenda.Id);

            if (dbEncomenda != null)
            {
                dbEncomenda.PossuiChamada = encomenda.PossuiChamada;
                dbEncomenda.TipoEncomenda = encomenda.TipoEncomenda;
                dbEncomenda.Titulo = encomenda.Titulo;
                dbEncomenda.Descricao = encomenda.Descricao;
                dbEncomenda.StatusEncomenda = encomenda.StatusEncomenda;
                dbEncomenda.Chamada = encomenda.Chamada;
                dbEncomenda.IdDemandante = encomenda.IdDemandante;
                dbEncomenda.IdSolucionador = encomenda.IdSolucionador;
                dbEncomenda.EncomendaAberta = encomenda.EncomendaAberta;

                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<List<Proposta>> GetPropostasByEncomenda(int id)
        {
            var chamada = await _context.Chamada.FirstOrDefaultAsync(c => c.EncomendaId == id);
            List<Proposta> propostas = await _context.Proposta.Where(p => p.ChamadaId == chamada.Id).ToListAsync();
            return propostas;
        }
        
        public async Task<bool> CheckDateExpiration(int id)
        {
            var chamada = await _context.Chamada.FirstOrDefaultAsync(c => c.EncomendaId == id);
            if (DateTime.Compare(chamada.DataValidade + TimeSpan.FromDays(1), DateTime.Now)<=0)
            {
                return true;
            }
            return false;
        }

        public async Task ExcluirProposta(int Id)
        {
            var entity = await _context.Set<Proposta>().FirstOrDefaultAsync(n => n.Id == Id);
            EntityEntry entityEntry = _context.Entry<Proposta>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
