using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace apoemaMatch.Migrations
{
    public partial class CorrecaoEncomenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServicoBuscado",
                table: "Encomendas",
                newName: "StatusEncomenda");

            migrationBuilder.RenameColumn(
                name: "AreaServico",
                table: "Encomendas",
                newName: "SegmentoDeMercado");

            migrationBuilder.AddColumn<int>(
                name: "AreaSolucaoBuscada",
                table: "Encomendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RealizaProcessoSeletivo",
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
                    Pergunta = table.Column<string>(type: "text", nullable: true),
                    TipoResposta = table.Column<int>(type: "integer", nullable: false),
                    EncomendaId = table.Column<int>(type: "integer", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Questao_EncomendaId",
                table: "Questao",
                column: "EncomendaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questao");

            migrationBuilder.DropColumn(
                name: "AreaSolucaoBuscada",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "RealizaProcessoSeletivo",
                table: "Encomendas");

            migrationBuilder.RenameColumn(
                name: "StatusEncomenda",
                table: "Encomendas",
                newName: "ServicoBuscado");

            migrationBuilder.RenameColumn(
                name: "SegmentoDeMercado",
                table: "Encomendas",
                newName: "AreaServico");
        }
    }
}
