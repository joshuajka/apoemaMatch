using Microsoft.EntityFrameworkCore.Migrations;

namespace apoemaMatch.Migrations
{
    public partial class Initial020922 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Solucionadores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Demandantes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "Demandantes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Solucionadores");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Demandantes");

            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "Demandantes");
        }
    }
}
