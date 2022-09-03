using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.Enums
{
    public enum EnumStatusEncomenda
    {
        Inicial,  // Demandante criou a encomenda, //Quando a encomenda foi publicada porém ainda falta o agenciador do APOEMA validar
        [Display(Name = "Analisando encomenda")]
        AnalisandoEncomenda, //Agenciador está analisando a encomenda
        Recusada, // Agenciador recusou a encomenda,
        Aberta, // Encomenda disponível para os solucionaores, //Já foi validado pelo agenciador do APOEMA e está diponível para receber as propostas
        [Display(Name = "Aguardando análise da chamada")]
        AguardandoAnaliseChamada, //Quando o prazo para enviar a proposta foi finalizado e está aguardando o demandante avaliar as propostas enviadas
        Finalizada // encomenda deixou de ser disponível //Encomenda já vinculada a um solucionador seja por vinculação direta ou indireta
    }
}
