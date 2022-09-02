using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace apoemaMatch.Migrations
{
    public partial class NovasModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encomendas_Demandantes_DemandanteId",
                table: "Encomendas");

            migrationBuilder.DropTable(
                name: "OpcaoResposta");

            migrationBuilder.DropTable(
                name: "Questao");

            migrationBuilder.DropColumn(
                name: "AreaSolucaoBuscada",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "EncomendaAberta",
                table: "Encomendas");

            migrationBuilder.RenameColumn(
                name: "SegmentoDeMercado",
                table: "Encomendas",
                newName: "TipoEncomenda");

            migrationBuilder.RenameColumn(
                name: "RealizaProcessoSeletivo",
                table: "Encomendas",
                newName: "PossuiChamada");

            migrationBuilder.AlterColumn<int>(
                name: "DemandanteId",
                table: "Encomendas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Encomendas",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "JustificativaRecusa",
                table: "Encomendas",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chamada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EncomendaId = table.Column<int>(type: "integer", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DescricaoChamada = table.Column<string>(type: "text", nullable: true),
                    ArquivoAnexo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chamada_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Criterio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    TipoCriterio = table.Column<int>(type: "integer", nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    ChamadaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterio_Chamada_ChamadaId",
                        column: x => x.ChamadaId,
                        principalTable: "Chamada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proposta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChamadaId = table.Column<int>(type: "integer", nullable: false),
                    StatusProposta = table.Column<int>(type: "integer", nullable: false),
                    SolucionadorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposta_Chamada_ChamadaId",
                        column: x => x.ChamadaId,
                        principalTable: "Chamada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proposta_Solucionadores_SolucionadorId",
                        column: x => x.SolucionadorId,
                        principalTable: "Solucionadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RespostaCriterio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CriterioId = table.Column<int>(type: "integer", nullable: false),
                    RespostaUpload = table.Column<string>(type: "text", nullable: true),
                    RespostaTextual = table.Column<string>(type: "text", nullable: true),
                    Nota = table.Column<int>(type: "integer", nullable: false),
                    PropostaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostaCriterio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostaCriterio_Criterio_CriterioId",
                        column: x => x.CriterioId,
                        principalTable: "Criterio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespostaCriterio_Proposta_PropostaId",
                        column: x => x.PropostaId,
                        principalTable: "Proposta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpcaoCriterio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Texto = table.Column<string>(type: "text", nullable: true),
                    CriterioId = table.Column<int>(type: "integer", nullable: true),
                    RespostaCriterioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcaoCriterio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcaoCriterio_Criterio_CriterioId",
                        column: x => x.CriterioId,
                        principalTable: "Criterio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpcaoCriterio_RespostaCriterio_RespostaCriterioId",
                        column: x => x.RespostaCriterioId,
                        principalTable: "RespostaCriterio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamada_EncomendaId",
                table: "Chamada",
                column: "EncomendaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Criterio_ChamadaId",
                table: "Criterio",
                column: "ChamadaId");

            migrationBuilder.CreateIndex(
                name: "IX_OpcaoCriterio_CriterioId",
                table: "OpcaoCriterio",
                column: "CriterioId");

            migrationBuilder.CreateIndex(
                name: "IX_OpcaoCriterio_RespostaCriterioId",
                table: "OpcaoCriterio",
                column: "RespostaCriterioId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposta_ChamadaId",
                table: "Proposta",
                column: "ChamadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposta_SolucionadorId",
                table: "Proposta",
                column: "SolucionadorId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostaCriterio_CriterioId",
                table: "RespostaCriterio",
                column: "CriterioId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostaCriterio_PropostaId",
                table: "RespostaCriterio",
                column: "PropostaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encomendas_Demandantes_DemandanteId",
                table: "Encomendas",
                column: "DemandanteId",
                principalTable: "Demandantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encomendas_Demandantes_DemandanteId",
                table: "Encomendas");

            migrationBuilder.DropTable(
                name: "OpcaoCriterio");

            migrationBuilder.DropTable(
                name: "RespostaCriterio");

            migrationBuilder.DropTable(
                name: "Criterio");

            migrationBuilder.DropTable(
                name: "Proposta");

            migrationBuilder.DropTable(
                name: "Chamada");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "JustificativaRecusa",
                table: "Encomendas");

            migrationBuilder.RenameColumn(
                name: "TipoEncomenda",
                table: "Encomendas",
                newName: "SegmentoDeMercado");

            migrationBuilder.RenameColumn(
                name: "PossuiChamada",
                table: "Encomendas",
                newName: "RealizaProcessoSeletivo");

            migrationBuilder.AlterColumn<int>(
                name: "DemandanteId",
                table: "Encomendas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "AreaSolucaoBuscada",
                table: "Encomendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "EncomendaAberta",
                table: "Encomendas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Questao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EncomendaId = table.Column<int>(type: "integer", nullable: true),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    Pergunta = table.Column<string>(type: "text", nullable: true),
                    TipoResposta = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questao_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpcaoResposta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EhRespostaEsperada = table.Column<bool>(type: "boolean", nullable: false),
                    QuestaoId = table.Column<int>(type: "integer", nullable: true),
                    Texto = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcaoResposta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcaoResposta_Questao_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpcaoResposta_QuestaoId",
                table: "OpcaoResposta",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Questao_EncomendaId",
                table: "Questao",
                column: "EncomendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encomendas_Demandantes_DemandanteId",
                table: "Encomendas",
                column: "DemandanteId",
                principalTable: "Demandantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
