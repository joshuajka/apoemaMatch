using apoemaMatch.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apoemaMatch.Data.ViewModels
{
    public class DemandanteViewModel
    {

        public int Id { get; set; }

        [Display(Name = "URL da imagem")]
        [Required(ErrorMessage = "URL da imagem é obrigatória")]
        public string ImagemURL { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "CNPJ é obrigatório")]
        public string Cnpj { get; set; }

        [Display(Name = "Nome Demandante")]
        [Required(ErrorMessage = "Nome Demandante é obrigatório")]
        public string NomeDemandante { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; }

        [Display(Name = "Nome Empresa")]
        [Required(ErrorMessage = "Nome da empresa é obrigatório")]
        public string NomeEmpresa { get; set; }

        [Display(Name = "Cargo demandante")]
        [Required(ErrorMessage = "Cargo demandante é obrigatório")]
        public string CargoDemandante { get; set; }

        [Display(Name = "Tempo de mercado")]
        [Required(ErrorMessage = "Tempo de mercado é obrigatório")]
        public int TempoDeMercado { get; set; }

        [Display(Name = "Porte da empresa")]
        [Required(ErrorMessage = "Porte da empresa é obrigatório")]
        public EnumPorteDaEmpresa PorteDaEmpresa { get; set; }

        [Display(Name = "Ramo de atuação")]
        [Required(ErrorMessage = "Ramo de atuação é obrigatório")]
        public EnumRamoDeAtuacao RamoDeAtuacao { get; set; }

        [Display(Name = "Segmento de mercado")]
        [Required(ErrorMessage = "Segmento de mercado é obrigatório")]
        public EnumSegmentoDeMercado SegmentoDeMercado { get; set; }

        [Display(Name = "Linha atuação")]
        [Required(ErrorMessage = "Linha atuação é obrigatório")]
        public EnumLinhaDeAtuacaoTI LinhaDeAtuacaoTI { get; set; }

        [Display(Name = "Tributação")]
        [Required(ErrorMessage = "Tributação é obrigatório")]
        public EnumTributacao RegimeDeTributacao { get; set; }

        [Display(Name = "Lei de informática")]
        [Required(ErrorMessage = "Lei de informática é obrigatória")]
        public EnumLeiDeInformatica LeiDeInformatica { get; set; }

        [Display(Name = "Objetivo")]
        [Required(ErrorMessage = "Objetivo é obrigatório")]
        public EnumObjetivoParceria ObjetivoParceria { get; set; }

        [Display(Name = "Área buscada")]
        [Required(ErrorMessage = "Área buscada é obrigatória")]
        public EnumAreaSolucaoBuscada AreaSolucaoBuscada { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descricao { get; set; }

        public int SolucionadorId { get; set; }

    }
}
