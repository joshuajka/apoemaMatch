using Microsoft.EntityFrameworkCore.Migrations;

namespace apoemaMatch.Migrations
{
    public partial class CorrecoesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encomenda_Demandantes_DemandanteId",
                table: "Encomenda");

            migrationBuilder.DropForeignKey(
                name: "FK_EncomendasSolucionadores_Encomenda_EncomendaId",
                table: "EncomendasSolucionadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Encomenda",
                table: "Encomenda");

            migrationBuilder.RenameTable(
                name: "Encomenda",
                newName: "Encomendas");

            migrationBuilder.RenameIndex(
                name: "IX_Encomenda_DemandanteId",
                table: "Encomendas",
                newName: "IX_Encomendas_DemandanteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Encomendas",
                table: "Encomendas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Encomendas_Demandantes_DemandanteId",
                table: "Encomendas",
                column: "DemandanteId",
                principalTable: "Demandantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EncomendasSolucionadores_Encomendas_EncomendaId",
                table: "EncomendasSolucionadores",
                column: "EncomendaId",
                principalTable: "Encomendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encomendas_Demandantes_DemandanteId",
                table: "Encomendas");

            migrationBuilder.DropForeignKey(
                name: "FK_EncomendasSolucionadores_Encomendas_EncomendaId",
                table: "EncomendasSolucionadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Encomendas",
                table: "Encomendas");

            migrationBuilder.RenameTable(
                name: "Encomendas",
                newName: "Encomenda");

            migrationBuilder.RenameIndex(
                name: "IX_Encomendas_DemandanteId",
                table: "Encomenda",
                newName: "IX_Encomenda_DemandanteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Encomenda",
                table: "Encomenda",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Encomenda_Demandantes_DemandanteId",
                table: "Encomenda",
                column: "DemandanteId",
                principalTable: "Demandantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EncomendasSolucionadores_Encomenda_EncomendaId",
                table: "EncomendasSolucionadores",
                column: "EncomendaId",
                principalTable: "Encomenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
