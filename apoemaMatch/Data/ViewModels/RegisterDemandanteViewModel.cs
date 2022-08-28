using apoemaMatch.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.ViewModels
{
    public class RegisterDemandanteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "URL Foto")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string ImagemURL { get; set; }

        [Display(Name = "Telefone (Com DDD)")]
        [MaxLength(11)]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{5})[-. ]?([0-9]{4})$",
                   ErrorMessage = "O número de telefone inserido não é válido. Exemplo: (12) 9 1234-5678 (Insira somente números)")]
        public string Telefone { get; set; }

        [Display(Name = "Nome da Empresa")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string NomeEmpresa { get; set; }

        [Display(Name = "Cargo do usuario")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CargoDemandante { get; set; }

        [Display(Name = "Tempo de mercado")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int TempoDeMercado { get; set; }

        [Display(Name = "Porte da empresa")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public EnumPorteDaEmpresa PorteDaEmpresa { get; set; }

        [Display(Name = "Ramo de atuação")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public EnumRamoDeAtuacao RamoDeAtuacao { get; set; }

        [Display(Name = "Segmento de mercado")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; }

        [Display(Name = "Linha de atuação TI")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public EnumLinhaDeAtuacaoTI LinhaDeAtuacaoTI { get; set; }

        [Display(Name = "Regime de tributação")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public EnumTributacao RegimeDeTributacao { get; set; }

        [Display(Name = "Lei de informática")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public EnumLeiDeInformatica LeiDeInformatica { get; set; }

        [Display(Name = "Objetivo da parceria")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public EnumObjetivoParceria ObjetivoParceria { get; set; }

        [Display(Name = "Área da solução buscada")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }

        [Display(Name = "Breve descrição da empresa")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Descricao { get; set; }

        [Display(Name = "Nome de usuário")]
        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "O nome só pode conter letras em uma única palavra, sugerimos seu primeiro nome")]
        public string UserName { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "Nome Completo é obrigatório")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "O nome só pode conter letras")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$", ErrorMessage = "Escreva um email válido")]
        public string Email { get; set; }

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
