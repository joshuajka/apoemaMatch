using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name="Nome")]
        public string Nome { get; set; }
    }
}
