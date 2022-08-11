using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data.Enums
{
    public enum EnumPorteDaEmpresa
    {
        MEI = 1, //Microempreendedor Individual (MEI)
        EIRELI = 2, //Empresa Individual de Responsabilidade Limitada (EIRELI)
        SS = 3,  //Sociedade Simples (SS)
        LTDA = 4,  //Sociedade Limitada (LTDA)
        EI = 5, //Empresário Individual (EI)
        SLU = 6, //Sociedade Limitada Unipessoal (SLU)
        SA = 7, //Sociedade Anônima (SA)
        OUTRO = 8
    }
}
