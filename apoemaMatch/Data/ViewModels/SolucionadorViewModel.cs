using apoemaMatch.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class SolucionadorViewModel
    {

        public int Id { get; set; }

        [Display(Name = "URL da Imagem")]
        [Required(ErrorMessage = "URL da Imagem é obrigatória")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        public bool Disponivel { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Cpf é obrigatório")]
        public string Cpf { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

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

    }
}
