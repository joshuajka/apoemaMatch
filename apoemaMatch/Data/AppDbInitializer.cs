using apoemaMatch.Data.Enums;
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
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                context.Database.EnsureCreated();
                //Demandante
                if (!context.Demandantes.Any())
                {
                    context.Demandantes.AddRange(new List<Demandante>()
                    {
                        new Demandante()
                        {
                            ImagemURL = "https://files.cercomp.ufg.br/weby/up/1218/o/apoema.png",
                            Email = "apoema@inf.ufg.br",
                            NomeDemandante = "Instituto Apoema",
                            Telefone = "+556235211724",
                            NomeEmpresa = "Apoema",
                            CargoDemandante = "Administrador Apoema",
                            TempoDeMercado = 3,
                            PorteDaEmpresa = EnumPorteDaEmpresa.MEI,
                            RamoDeAtuacao = EnumRamoDeAtuacao.Industria,
                            SegmentoDeMercado = EnumSegmentoDeMercado.InformaticaTI,
                            LinhaDeAtuacaoTI = EnumLinhaDeAtuacaoTI.Outros,
                            RegimeDeTributacao = EnumTributacao.LucroPresumido,
                            LeiDeInformatica = EnumLeiDeInformatica.Nao_Usa,
                            ObjetivoParceria = EnumObjetivoParceria.Pesquisa_Desenvolvimento_Inovacao,
                            AreaSolucaoBuscada = EnumAreaSolucaoBuscada.Inteligencia_Artificial_Aplicada,
                            Descricao = "Instituto APOEMA é um órgão complementar de pesquisa e desenvolvimento vinculado ao Instituto de Informática (INF) " +
                            "da Universidade Federal de Goiás (UFG) que promove, através de projetos, a incorporação de novas tecnologias, " +
                            "processos e serviços que contribuem para a consolidação do pólo de Tecnologia da Informação e Comunicação (TIC) de Goiás."
                        }

                    });
                    context.SaveChanges();
                }
            //    //Solucionador
            //    if (!context.Solucionadores.Any())
            //    {
            //        context.Solucionadores.AddRange(new List<Solucionador>()
            //        {
            //            new Solucionador()
            //            {
            //                Disponivel = true,
            //                ImagemURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7e/Marie_Curie_c1920.jpg/200px-Marie_Curie_c1920.jpg",
            //                Email = "marie@mail.com",
            //                Nome = "Marie Curie",
            //                Telefone = "+68925647897",
            //                Formacao = "Química",
            //                AreaDePesquisa = EnumAreaSolucaoBuscada.Inteligencia_Artificial_Aplicada,
            //                CurriculoLattes = "Lattes",
            //                MiniBio = "Solucionador teste"
            //            }
            //        });

            //        context.SaveChanges();

            //    }

            //    //Encomenda
            //    if (!context.Encomendas.Any())
            //    {
            //        context.Encomendas.AddRange(new List<Encomenda>()
            //        {
            //            new Encomenda()
            //            {
            //                IdDemandante = 1,
            //                EncomendaAberta = true,
            //                Titulo = "Nova Encomenda",
            //                TipoEncomenda = EnumTipoEncomenda.DesenvolvimentoTecnologico,
            //                Descricao = "Essa é uma nova encomenda",
            //                StatusEncomenda = EnumStatusEncomenda.Aberta,
            //                Demandante = context.Demandantes.First()
            //            }
            //        });

            //        context.SaveChanges();
            //    }

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
                string adminUserEmail = "apoema@inf.ufg.br";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var novoUsuarioAdmin = new ApplicationUser()
                    {
                        Nome = "Usuario Admin",
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(novoUsuarioAdmin, "Coding@1234?");
                    await userManager.AddToRoleAsync(novoUsuarioAdmin, PapeisUsuarios.Admin);
                }

                ////Usuario genérico
                //string appUserEmail = "user@apoema.com";
                //var appUser = await userManager.FindByEmailAsync(appUserEmail);
                //if (appUser == null)
                //{
                //    var novoAppUser = new ApplicationUser()
                //    {
                //        Nome = "Usuario",
                //        UserName = "user",
                //        Email = appUserEmail,
                //        EmailConfirmed = true
                //    };
                //    await userManager.CreateAsync(novoAppUser, "Coding@1234?");
                //    await userManager.AddToRoleAsync(novoAppUser, PapeisUsuarios.User);
                //}

                ////Solucionador 
                //string appSolucionadorEmail = "solucionador@apoema.com";
                //var solucionadorUser = await userManager.FindByEmailAsync(appSolucionadorEmail);
                //if (solucionadorUser == null)
                //{
                //    var novoSolucionadorUser = new ApplicationUser()
                //    {
                //        Nome = "solucionador",
                //        UserName = "solucionadorUser",
                //        Email = appSolucionadorEmail,
                //        EmailConfirmed = true
                //    };
                //    await userManager.CreateAsync(novoSolucionadorUser, "Coding@1234?");
                //    await userManager.AddToRoleAsync(novoSolucionadorUser, PapeisUsuarios.Solucionador);
                //}
                ////Demandante
                //string appDemandanteEmail = "demandante@apoema.com";
                //var demandanteUser = await userManager.FindByEmailAsync(appDemandanteEmail);
                //if (demandanteUser == null)
                //{
                //    var novoDemandanteUser = new ApplicationUser()
                //    {
                //        Nome = "demandante",
                //        UserName = "demandanteUser",
                //        Email = appDemandanteEmail,
                //        EmailConfirmed = true
                //    };
                //    await userManager.CreateAsync(novoDemandanteUser, "Coding@1234?");
                //    await userManager.AddToRoleAsync(novoDemandanteUser, PapeisUsuarios.Demandante);
                //}
            }
        }

    }
}
