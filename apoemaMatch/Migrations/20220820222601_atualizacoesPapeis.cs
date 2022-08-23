using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace apoemaMatch.Migrations
{
    public partial class atualizacoesPapeis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DemandaAberta",
                table: "Demandas");

            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "Solucionadores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "Demandas",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Encomendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: true),
                    AreaServico = table.Column<int>(type: "integer", nullable: false),
                    ServicoBuscado = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encomendas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Encomendas");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Solucionadores");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Demandas");

            migrationBuilder.AddColumn<bool>(
                name: "DemandaAberta",
                table: "Demandas",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
