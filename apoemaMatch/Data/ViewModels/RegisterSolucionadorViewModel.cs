﻿using apoemaMatch.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.ViewModels
{
    public class RegisterSolucionadorViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Foto")]
        [Required(ErrorMessage = "Imagem é obrigatória")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$", ErrorMessage = "Escreva um email válido")]
        public string Email { get; set; }

        [Display(Name = "Nome de usuário")]
        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        [RegularExpression("[A-Za-z]*", ErrorMessage = "O nome só pode conter letras em uma única palavra, sugerimos seu primeiro nome")]
        public string UserName { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "Nome Completo é obrigatório")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "O nome só pode conter letras")]
        public string NomeCompleto { get; set; }


        [Required(ErrorMessage = "Telefone é obrigatório")]
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
        [Compare("Password",ErrorMessage = "Senhas não coincidem")]
        public string ConfirmPassword { get; set; } 

    }
}
