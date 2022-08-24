using Microsoft.EntityFrameworkCore.Migrations;

namespace apoemaMatch.Migrations
{
    public partial class vincularSolucionador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EncomendaAberta",
                table: "Encomendas",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncomendaAberta",
                table: "Encomendas");
        }
    }
}
