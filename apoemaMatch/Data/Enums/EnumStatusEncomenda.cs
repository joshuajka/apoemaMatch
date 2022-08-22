using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.Enums
{
    public enum EnumStatusEncomenda
    {
        Iniciado,  // Demandante criou a encomenda,
        Recusado, // Agenciador recusou a encomenda,
        Publico, // Encomenda disponível para os solucionaores,
        Finalizado // encomenda deixou de ser disponível
    }
}
