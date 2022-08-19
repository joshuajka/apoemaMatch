using System.Threading.Tasks;
using apoemaMatch.Data.Base;
using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.EntityFrameworkCore;

namespace apoemaMatch.Data.Services
{
    public class EncomendaService: EntityBaseRepository<Encomenda>, IEncomendaService
    {
        
        private readonly AppDbContext _context;

        public EncomendaService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<Encomenda> GetEncomendaByIdAsync(int Id)
        {
            return await _context.Encomendas.FirstOrDefaultAsync(n => n.Id == Id);
        }

        public async Task AdicionarEncomendaAsync(EncomendaViewModel encomenda)
        {
            var novaEncomenda = new Encomenda()
            {
                Titulo = encomenda.Titulo,
                SegmentoDeMercado = encomenda.SegmentoDeMercado,
                AreaSolucaoBuscada = encomenda.AreaSolucaoBuscada,
                Descricao = encomenda.Descricao,
                RealizaProcessoSeletivo = encomenda.RealizaProcessoSeletivo
            };
            await _context.Encomendas.AddAsync(novaEncomenda);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateEncomendaAsync(EncomendaViewModel encomenda)
        {
            var dbencomenda = await _context.Encomendas.FirstOrDefaultAsync(n => n.Id == encomenda.Id);

            if(dbencomenda != null)
            {
                dbencomenda.Titulo = encomenda.Titulo;
                dbencomenda.SegmentoDeMercado = encomenda.SegmentoDeMercado;
                dbencomenda.AreaSolucaoBuscada = encomenda.AreaSolucaoBuscada;
                dbencomenda.Descricao = encomenda.Descricao;
                dbencomenda.RealizaProcessoSeletivo = encomenda.RealizaProcessoSeletivo;
                await _context.SaveChangesAsync();
            }
            
            //TODO(Se alterar o campo Realiza processo seletivo, excluir os questionarios)

        }

    }
}