using apoemaMatch.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class RegisterSolucionadorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Url da imagem")]
        [Required(ErrorMessage = "Url da imagem é obrigatória")]
        public string ImagemURL { get; set; }

        public bool Disponivel { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "CPF é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$", ErrorMessage = "Escreva um email válido")]
        public string Email { get; set; }

        [Display(Name = "Nome de usuário")]
        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        //[RegularExpression("[A-Za-z]*", ErrorMessage = "O nome só pode conter letras em uma única palavra sem acentos, sugerimos seu primeiro nome")]
        public string UserName { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "Nome Completo é obrigatório")]
        [RegularExpression(@"^[a-zA-ZÀ-ú''-'\s]{1,40}$", ErrorMessage = "O nome só pode conter letras")]
        public string NomeCompleto { get; set; }


        [Display(Name = "Telefone (Com DDD)")]
        [MaxLength(11)]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{4,5})[-. ]?([0-9]{4})$",
                   ErrorMessage = "O número de telefone inserido não é válido. Exemplo: (12) 9 1234-5678 ou (12) 1234-5678 (Insira somente números)")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Formação é obrigatório")]
        [Display(Name = "Formação")]
        public string Formacao { get; set; }

        [Required(ErrorMessage = "Área de pesquisa é obrigatória")]
        [Display(Name = "Área de Pesquisa")]
        public EnumAreaSolucaoBuscada AreaDePesquisa { get; set; }

        [Required(ErrorMessage = "Currículo Lattes é obrigatório")]
        [Display(Name = "Currículo Lattes")]
        public string CurriculoLattes { get; set; }

        [Display(Name = "Bio")]
        [Required(ErrorMessage = "Bio é obrigatória")]
        public string MiniBio { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "A senha deve conter no mínimo 6 dígitos, com 1 caractere especial, 1 numérico, 1 letra maiúscula e 1 letra miníscula")]
        public string Password { get; set; }

        [Display(Name = "Senha de confirmação")]
        [Required(ErrorMessage = "Senha de confirmação é obrigatória")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senhas não coincidem")]
        public string ConfirmPassword { get; set; }

    }
}
