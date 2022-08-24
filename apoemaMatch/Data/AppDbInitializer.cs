﻿using apoemaMatch.Data.Enums;
using apoemaMatch.Data.Static;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
                //Demandante
                if (!context.Demandantes.Any())
                {
                    context.Demandantes.AddRange(new List<Demandante>()
                    {
                        new Demandante()
                        {
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
                            Descricao = "Demandante teste"
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

                //Encomenda
                if (!context.Encomendas.Any())
                {
                    context.Encomendas.AddRange(new List<Encomenda>()
                    {
                        new Encomenda()
                        {
                            EncomendaAberta = true,
                            Titulo = "",
                            SegmentoDeMercado = EnumSegmentoDeMercado.Educacao,
                            AreaSolucaoBuscada = EnumAreaSolucaoBuscada.Bioinformatica,
                            Descricao = "",
                            StatusEncomenda = EnumStatusEncomenda.Iniciado
                        }
                    });

                    context.SaveChanges();
                }

                //EncomendaSolucionador
                if (!context.EncomendasSolucionadores.Any())
                {
                    context.EncomendasSolucionadores.AddRange(new List<EncomendaSolucionador>()
                    {

                        new EncomendaSolucionador()
                        {
                            EncomendaId = 1,
                            SolucionadorId =1
                        }

                    });
                    context.SaveChanges();
                }

            }
        }

        public static async Task SeedUsuariosEPapeisAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Papeis
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(PapeisUsuarios.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(PapeisUsuarios.Admin));
                }

                if (!await roleManager.RoleExistsAsync(PapeisUsuarios.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(PapeisUsuarios.User));
                }

                if (!await roleManager.RoleExistsAsync(PapeisUsuarios.Solucionador))
                {
                    await roleManager.CreateAsync(new IdentityRole(PapeisUsuarios.Solucionador));
                }

                if (!await roleManager.RoleExistsAsync(PapeisUsuarios.Demandante))
                {
                    await roleManager.CreateAsync(new IdentityRole(PapeisUsuarios.Demandante));
                }

                //Usuarios
                //Admin
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@apoema.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var novoUsuarioAdmin = new ApplicationUser()
                    {
                        Nome = "Usuario Admin",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(novoUsuarioAdmin, "Coding@1234?");
                    await userManager.AddToRoleAsync(novoUsuarioAdmin, PapeisUsuarios.Admin);
                }

                //Usuario genérico
                string appUserEmail = "user@apoema.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var novoAppUser = new ApplicationUser()
                    {
                        Nome = "Usuario",
                        UserName = "user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(novoAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(novoAppUser, PapeisUsuarios.User);
                }

                //Solucionador 
                string appSolucionadorEmail = "solucionador@apoema.com";
                var solucionadorUser = await userManager.FindByEmailAsync(appSolucionadorEmail);
                if (solucionadorUser == null)
                {
                    var novoSolucionadorUser = new ApplicationUser()
                    {
                        Nome = "solucionador",
                        UserName = "solucionadorUser",
                        Email = appSolucionadorEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(novoSolucionadorUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(novoSolucionadorUser, PapeisUsuarios.Solucionador);
                }
                //Demandante
                string appDemandanteEmail = "demandante@apoema.com";
                var demandanteUser = await userManager.FindByEmailAsync(appDemandanteEmail);
                if (demandanteUser == null)
                {
                    var novoDemandanteUser = new ApplicationUser()
                    {
                        Nome = "demandante",
                        UserName = "demandanteUser",
                        Email = appDemandanteEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(novoDemandanteUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(novoDemandanteUser, PapeisUsuarios.Demandante);
                }
            }
        }

    }
}
