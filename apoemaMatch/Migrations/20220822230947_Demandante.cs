using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace apoemaMatch.Migrations
{
    public partial class Demandante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandasSolucionadores");

            migrationBuilder.DropTable(
                name: "Demandas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Encomendas",
                table: "Encomendas");

            migrationBuilder.RenameTable(
                name: "Encomendas",
                newName: "Encomenda");

            migrationBuilder.AddColumn<int>(
                name: "DemandanteId",
                table: "Encomenda",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Encomenda",
                table: "Encomenda",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Demandantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImagemURL = table.Column<string>(type: "text", nullable: true),
                    IdUsuario = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NomeDemandante = table.Column<string>(type: "text", nullable: true),
                    Telefone = table.Column<string>(type: "text", nullable: true),
                    NomeEmpresa = table.Column<string>(type: "text", nullable: true),
                    CargoDemandante = table.Column<string>(type: "text", nullable: true),
                    TempoDeMercado = table.Column<int>(type: "integer", nullable: false),
                    PorteDaEmpresa = table.Column<int>(type: "integer", nullable: false),
                    RamoDeAtuacao = table.Column<int>(type: "integer", nullable: false),
                    SegmentoDeMercado = table.Column<int>(type: "integer", nullable: false),
                    LinhaDeAtuacaoTI = table.Column<int>(type: "integer", nullable: false),
                    RegimeDeTributacao = table.Column<int>(type: "integer", nullable: false),
                    LeiDeInformatica = table.Column<int>(type: "integer", nullable: false),
                    ObjetivoParceria = table.Column<int>(type: "integer", nullable: false),
                    AreaSolucaoBuscada = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demandantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EncomendasSolucionadores",
                columns: table => new
                {
                    EncomendaId = table.Column<int>(type: "integer", nullable: false),
                    SolucionadorId = table.Column<int>(type: "integer", nullable: false),
                    DemandanteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncomendasSolucionadores", x => new { x.EncomendaId, x.SolucionadorId });
                    table.ForeignKey(
                        name: "FK_EncomendasSolucionadores_Demandantes_DemandanteId",
                        column: x => x.DemandanteId,
                        principalTable: "Demandantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EncomendasSolucionadores_Encomenda_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncomendasSolucionadores_Solucionadores_SolucionadorId",
                        column: x => x.SolucionadorId,
                        principalTable: "Solucionadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encomenda_DemandanteId",
                table: "Encomenda",
                column: "DemandanteId");

            migrationBuilder.CreateIndex(
                name: "IX_EncomendasSolucionadores_DemandanteId",
                table: "EncomendasSolucionadores",
                column: "DemandanteId");

            migrationBuilder.CreateIndex(
                name: "IX_EncomendasSolucionadores_SolucionadorId",
                table: "EncomendasSolucionadores",
                column: "SolucionadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encomenda_Demandantes_DemandanteId",
                table: "Encomenda",
                column: "DemandanteId",
                principalTable: "Demandantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encomenda_Demandantes_DemandanteId",
                table: "Encomenda");

            migrationBuilder.DropTable(
                name: "EncomendasSolucionadores");

            migrationBuilder.DropTable(
                name: "Demandantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Encomenda",
                table: "Encomenda");

            migrationBuilder.DropIndex(
                name: "IX_Encomenda_DemandanteId",
                table: "Encomenda");

            migrationBuilder.DropColumn(
                name: "DemandanteId",
                table: "Encomenda");

            migrationBuilder.RenameTable(
                name: "Encomenda",
                newName: "Encomendas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Encomendas",
                table: "Encomendas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Demandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AreaSolucaoBuscada = table.Column<int>(type: "integer", nullable: false),
                    CargoDemandante = table.Column<string>(type: "text", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    IdUsuario = table.Column<string>(type: "text", nullable: true),
                    ImagemURL = table.Column<string>(type: "text", nullable: true),
                    LeiDeInformatica = table.Column<int>(type: "integer", nullable: false),
                    LinhaDeAtuacaoTI = table.Column<int>(type: "integer", nullable: false),
                    NomeDemandante = table.Column<string>(type: "text", nullable: true),
                    NomeEmpresa = table.Column<string>(type: "text", nullable: true),
                    ObjetivoParceria = table.Column<int>(type: "integer", nullable: false),
                    PorteDaEmpresa = table.Column<int>(type: "integer", nullable: false),
                    RamoDeAtuacao = table.Column<int>(type: "integer", nullable: false),
                    RegimeDeTributacao = table.Column<int>(type: "integer", nullable: false),
                    SegmentoDeMercado = table.Column<int>(type: "integer", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: true),
                    TempoDeMercado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demandas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DemandasSolucionadores",
                columns: table => new
                {
                    DemandaId = table.Column<int>(type: "integer", nullable: false),
                    SolucionadorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandasSolucionadores", x => new { x.DemandaId, x.SolucionadorId });
                    table.ForeignKey(
                        name: "FK_DemandasSolucionadores_Demandas_DemandaId",
                        column: x => x.DemandaId,
                        principalTable: "Demandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandasSolucionadores_Solucionadores_SolucionadorId",
                        column: x => x.SolucionadorId,
                        principalTable: "Solucionadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemandasSolucionadores_SolucionadorId",
                table: "DemandasSolucionadores",
                column: "SolucionadorId");
        }
    }
}
