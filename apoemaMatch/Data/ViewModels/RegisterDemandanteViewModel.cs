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
        [Display(Name = "URL Foto")]
        public string ImagemURL { get; set; }

        public string Telefone { get; set; }

        public string NomeEmpresa { get; set; }

        public string CargoDemandante { get; set; }

        public int TempoDeMercado { get; set; }

        public EnumPorteDaEmpresa PorteDaEmpresa { get; set; }

        public EnumRamoDeAtuacao RamoDeAtuacao { get; set; }

        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; }

        public EnumLinhaDeAtuacaoTI LinhaDeAtuacaoTI { get; set; }

        public EnumTributacao RegimeDeTributacao { get; set; }

        public EnumLeiDeInformatica LeiDeInformatica { get; set; }

        public EnumObjetivoParceria ObjetivoParceria { get; set; }

        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }

        public string Descricao { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Email é obrigatório")]
        public string Email { get; set; }

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
