using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class ConfirmacaoCadastroViewModel
    {
        [Required]
        public string UserId { get; set; }

        public bool EmailSent { get; set; }
    }
}
