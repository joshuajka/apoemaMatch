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

        [Required(ErrorMessage = "Este campo é obrigatório")]
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

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Email é obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Senha de confirmação")]
        [Required(ErrorMessage = "Senha de confirmação é obrigatória")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Senhas não coincidem")]
        public string ConfirmPassword { get; set; }

    }
}
