using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class OpcaoRespostaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Texto para primeiro atributo")]
        public string TextoPrimeiraOpcao { get; set; }

        [Display(Name = "Texto para segundo atributo")]
        public string TextoSegundaOpcao { get; set; }

        public bool EhRespostaEsperada { get; set; }
    }
}
