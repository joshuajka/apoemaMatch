using apoemaMatch.Data.Enums;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                //Demanda
                if (!context.Demandas.Any())
                {
                    context.Demandas.AddRange(new List<Demanda>()
                    {
                        new Demanda()
                        {
                            DemandaAberta = true,
                            ImagemURL = "",
                            Email = "",
                            NomeDemandante = "",
                            Telefone = "",
                            NomeEmpresa = "",
                            CargoDemandante = "",
                            TempoDeMercado = 3,
                            PorteDaEmpresa = EnumPorteDaEmpresa.MEI,
                            RamoDeAtuacao = EnumRamoDeAtuacao.Industria,
                            SegmentoDeMercado = EnumSegmentoDeMercado.AlimentosEBebidas,
                            LinhaDeAtuacaoTI = EnumLinhaDeAtuacaoTI.Desenvolvimento_Cloud_BigData,
                            RegimeDeTributacao = EnumTributacao.LucroPresumido,
                            LeiDeInformatica = EnumLeiDeInformatica.Nao_Usa,
                            ObjetivoParceria = EnumObjetivoParceria.Pesquisa_Desenvolvimento_Inovacao,
                            AreaSolucaoBuscada = EnumAreaSolucaoBuscada.Inteligencia_Artificial_Aplicada,
                            Descricao = "Demanda teste"
                        }

                    });
                    context.SaveChanges();
                }
                //Solucionador
                if (!context.Solucionadores.Any())
                {
                    context.Solucionadores.AddRange(new List<Solucionador>()
                    {
                        new Solucionador()
                        {
                            Disponivel = true,
                            ImagemURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Email = "",
                            Nome = "",
                            Telefone = "",
                            Formacao = "",
                            AreaDePesquisa = EnumAreaSolucaoBuscada.Inteligencia_Artificial_Aplicada,
                            CurriculoLattes = "Lattes",
                            MiniBio = "Solucionador teste"
                        }
                    });

                    context.SaveChanges();

                }
                //DemandaSolucionador
                if (!context.DemandasSolucionadores.Any())
                {
                    context.DemandasSolucionadores.AddRange(new List<DemandaSolucionador>()
                    {

                        new DemandaSolucionador()
                        {
                            DemandaId = 1,
                            SolucionadorId =1
                        }

                    });
                    context.SaveChanges();
                }

            }
        }
    }
}
