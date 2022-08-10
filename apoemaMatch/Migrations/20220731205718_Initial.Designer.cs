﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using apoemaMatch.Data;

namespace apoemaMatch.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220731205718_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("apoemaMatch.Models.Demanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaSolucaoBuscada")
                        .HasColumnType("int");

                    b.Property<string>("CargoDemandante")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DemandaAberta")
                        .HasColumnType("bit");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeiDeInformatica")
                        .HasColumnType("int");

                    b.Property<int>("LinhaDeAtuacaoTI")
                        .HasColumnType("int");

                    b.Property<string>("NomeDemandante")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeEmpresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ObjetivoParceria")
                        .HasColumnType("int");

                    b.Property<int>("PorteDaEmpresa")
                        .HasColumnType("int");

                    b.Property<int>("RamoDeAtuacao")
                        .HasColumnType("int");

                    b.Property<int>("RegimeDeTributacao")
                        .HasColumnType("int");

                    b.Property<int>("SegmentoDeMercado")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TempoDeMercado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Demandas");
                });

            modelBuilder.Entity("apoemaMatch.Models.DemandaSolucionador", b =>
                {
                    b.Property<int>("DemandaId")
                        .HasColumnType("int");

                    b.Property<int>("SolucionadorId")
                        .HasColumnType("int");

                    b.HasKey("DemandaId", "SolucionadorId");

                    b.HasIndex("SolucionadorId");

                    b.ToTable("DemandasSolucionadores");
                });

            modelBuilder.Entity("apoemaMatch.Models.Solucionador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaDePesquisa")
                        .HasColumnType("int");

                    b.Property<string>("CurriculoLattes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Formacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiniBio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Solucionadores");
                });

            modelBuilder.Entity("apoemaMatch.Models.DemandaSolucionador", b =>
                {
                    b.HasOne("apoemaMatch.Models.Demanda", "Demanda")
                        .WithMany("DemandaSolucionador")
                        .HasForeignKey("DemandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apoemaMatch.Models.Solucionador", "Solucionador")
                        .WithMany("DemandaSolucionador")
                        .HasForeignKey("SolucionadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Demanda");

                    b.Navigation("Solucionador");
                });

            modelBuilder.Entity("apoemaMatch.Models.Demanda", b =>
                {
                    b.Navigation("DemandaSolucionador");
                });

            modelBuilder.Entity("apoemaMatch.Models.Solucionador", b =>
                {
                    b.Navigation("DemandaSolucionador");
                });
#pragma warning restore 612, 618
        }
    }
}