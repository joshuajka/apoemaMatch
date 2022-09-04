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
                            proposta.RespostasCriterios.Add(respostaCriterio);
                        }
                    }

                    chamada.Criterios = criterios;
                    chamada.Propostas = propostas;
                    encomendaBuscada.Chamada = chamada;
                }
            }

            return encomendaBuscada;
        }
    }
}
