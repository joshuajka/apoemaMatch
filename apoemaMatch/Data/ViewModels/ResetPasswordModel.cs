using apoemaMatch.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class ResetPasswordModel
    {

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Display(Name = "Nova Senha")]
        [Required, DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "A senha deve conter no mínimo 6 dígitos, com 1 caractere especial, 1 numérico, 1 letra maiúscula e 1 letra miníscula")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmar Nova Senha")]
        [Required, DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }

        public bool IsSuccess { get; set; }

    }
}
