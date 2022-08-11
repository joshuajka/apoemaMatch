using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Enums
{
    public enum EnumAreaSolucaoBuscada
    {
        [Display(Name = "Qualidade de Software")]
        Qualidade_de_Software = 1,
        [Display(Name = "Recuperação de Informações")]
        Recuperacao_de_Informacoes,
        [Display(Name = "Redes")]
        Redes,
        [Display(Name = "Testes")]
        Testes,
        [Display(Name = "Web Semântica")]
        Web_Semantica,
        [Display(Name = "Inteligência Artificial Aplicada")]
        Inteligencia_Artificial_Aplicada,
        [Display(Name = "Sistema Web")]
        Sistemas_Web,
        [Display(Name = "Bioinformática")]
        Bioinformatica,
        [Display(Name = "Processamento de Imagens")]
        Processamento_de_Imagens,
        [Display(Name = "Teoria dos grafos")]
        Teoria_dos_Grafos,
        [Display(Name = "Otimização")]
        Otimizacao,
        [Display(Name = "Mineração de Dados")]
        Mineracao_de_Dados,
        [Display(Name = "Design")]
        Design,
        [Display(Name = "Sistemas Embarcados")]
        Sistemas_Embarcados,
        [Display(Name = "Geoprocessamento Distribuído")]
        Geoprocessamento_Distribuido,
        [Display(Name = "Cluster")]
        Cluster
    }
}
