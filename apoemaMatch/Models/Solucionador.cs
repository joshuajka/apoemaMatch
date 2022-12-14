using apoemaMatch.Data.Enums;
using apoemaMatch.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Models
{
    public class Solucionador : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string IdUsuario { get; set; }

        public bool Disponivel { get; set; }

        public string Cpf { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "Imagem é obrigatória")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

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
