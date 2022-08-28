
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.ViewModels
{
    public class MudarSenhaViewModel
    {
        [Required, DataType(DataType.Password), Display(Name = "Senha atual")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Nova senha")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "A senha deve conter no mínimo 6 dígitos, com 1 caractere especial, 1 numérico, 1 letra maiúscula e 1 letra miníscula")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirme a nova senha")]
        [Compare("NewPassword", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmNewPassword { get; set; }
    }
}