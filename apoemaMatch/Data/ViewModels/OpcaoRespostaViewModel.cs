using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class OpcaoRespostaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Texto para primeira opção")]
        public string TextoPrimeiraOpcao { get; set; }

        [Display(Name = "Texto para segunda opção")]
        public string TextoSegundaOpcao { get; set; }

        public bool EhRespostaEsperada { get; set; }
    }
}
