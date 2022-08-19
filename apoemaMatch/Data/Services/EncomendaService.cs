using apoemaMatch.Data.Base;
using apoemaMatch.Models;

namespace apoemaMatch.Data.Services
{
    public class EncomendaService : EntityBaseRepository<Encomenda>, IEncomendaService
    {
        public EncomendaService(AppDbContext context) : base(context) { }
    }
}
