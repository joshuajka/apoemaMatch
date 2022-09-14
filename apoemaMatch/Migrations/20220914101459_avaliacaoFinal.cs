using Microsoft.EntityFrameworkCore.Migrations;

namespace apoemaMatch.Migrations
{
    public partial class avaliacaoFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvaliacaoSolucionarSelecionadoChamada",
                table: "Encomendas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotaSolucionarSelecionadoChamada",
                table: "Encomendas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoEncomendaOutros",
                table: "Encomendas",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvaliacaoSolucionarSelecionadoChamada",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "NotaSolucionarSelecionadoChamada",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "TipoEncomendaOutros",
                table: "Encomendas");
        }
    }
}
