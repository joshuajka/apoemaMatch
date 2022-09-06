using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace apoemaMatch.Migrations
{
    public partial class relacoesModelsChamada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criterio_Chamada_ChamadaId",
                table: "Criterio");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposta_Solucionadores_SolucionadorId",
                table: "Proposta");

            migrationBuilder.DropForeignKey(
                name: "FK_RespostaCriterio_Proposta_PropostaId",
                table: "RespostaCriterio");

            migrationBuilder.DropTable(
                name: "OpcaoCriterio");

            migrationBuilder.AlterColumn<int>(
                name: "PropostaId",
                table: "RespostaCriterio",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "OpcoesSelecionadas",
                table: "RespostaCriterio",
                type: "text[]",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SolucionadorId",
                table: "Proposta",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChamadaId",
                table: "Criterio",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "OpcoesCriterios",
                table: "Criterio",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Criterio_Chamada_ChamadaId",
                table: "Criterio",
                column: "ChamadaId",
                principalTable: "Chamada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposta_Solucionadores_SolucionadorId",
                table: "Proposta",
                column: "SolucionadorId",
                principalTable: "Solucionadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RespostaCriterio_Proposta_PropostaId",
                table: "RespostaCriterio",
                column: "PropostaId",
                principalTable: "Proposta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criterio_Chamada_ChamadaId",
                table: "Criterio");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposta_Solucionadores_SolucionadorId",
                table: "Proposta");

            migrationBuilder.DropForeignKey(
                name: "FK_RespostaCriterio_Proposta_PropostaId",
                table: "RespostaCriterio");

            migrationBuilder.DropColumn(
                name: "OpcoesSelecionadas",
                table: "RespostaCriterio");

            migrationBuilder.DropColumn(
                name: "OpcoesCriterios",
                table: "Criterio");

            migrationBuilder.AlterColumn<int>(
                name: "PropostaId",
                table: "RespostaCriterio",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SolucionadorId",
                table: "Proposta",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ChamadaId",
                table: "Criterio",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "OpcaoCriterio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CriterioId = table.Column<int>(type: "integer", nullable: true),
                    RespostaCriterioId = table.Column<int>(type: "integer", nullable: true),
                    Texto = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcaoCriterio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcaoCriterio_Criterio_CriterioId",
                        column: x => x.CriterioId,
                        principalTable: "Criterio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpcaoCriterio_RespostaCriterio_RespostaCriterioId",
                        column: x => x.RespostaCriterioId,
                        principalTable: "RespostaCriterio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpcaoCriterio_CriterioId",
                table: "OpcaoCriterio",
                column: "CriterioId");

            migrationBuilder.CreateIndex(
                name: "IX_OpcaoCriterio_RespostaCriterioId",
                table: "OpcaoCriterio",
                column: "RespostaCriterioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Criterio_Chamada_ChamadaId",
                table: "Criterio",
                column: "ChamadaId",
                principalTable: "Chamada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposta_Solucionadores_SolucionadorId",
                table: "Proposta",
                column: "SolucionadorId",
                principalTable: "Solucionadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RespostaCriterio_Proposta_PropostaId",
                table: "RespostaCriterio",
                column: "PropostaId",
                principalTable: "Proposta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
