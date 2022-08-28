
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.ViewModels
{
    public class MudarEmailViewModel
    {
        [Required, Display(Name = "Email atual")]
        public string CurrentEmail { get; set; }

        [Required, Display(Name = "Novo Email")]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$", ErrorMessage = "Escreva um email válido")]
        public string NewEmail { get; set; }

        [Required, Display(Name = "Confirme o novo Email")]
        [Compare("NewEmail", ErrorMessage = "Os emails não coincidem")]
        public string ConfirmNewEmail { get; set; }
    }
}