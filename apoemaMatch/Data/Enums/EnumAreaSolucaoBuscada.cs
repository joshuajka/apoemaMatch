using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data
{
    public enum EnumAreaSolucaoBuscada
    {
        [Display(Name = "Qualidade de Software")]
        Qualidade_de_Software = 1,
        [Display(Name = "Recuperação de Informações")]
        Recuperacao_de_Informacoes,
        Redes,
        Testes,
        Web_Semantica,
        Inteligencia_Artificial_Aplicada,
        Sistemas_Web,
        Bioinformatica,
        Processamento_de_Imagens,
        Teoria_dos_Grafos,
        Otimizacao,
        Mineracao_de_Dados,
        Design,
        Sistemas_Embarcados,
        Geoprocessamento_Distribuido,
        Cluster
    }
}
