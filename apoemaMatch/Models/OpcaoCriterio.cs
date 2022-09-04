namespace apoemaMatch.Models
{
    public class OpcaoCriterio
    {
        public int Id { get; set; }

        public string Texto { get; set; }

        public int CriterioId { get; set; }

        public Criterio Criterio { get; set; }
    }
}
