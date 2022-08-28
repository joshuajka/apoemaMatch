using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.ViewModels
{
    public class EsqueciSenhaViewModel
    {
        [Required, EmailAddress, Display(Name = "Email cadastrado")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}