using Microsoft.EntityFrameworkCore.Migrations;

namespace apoemaMatch.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Demandas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemandaAberta = table.Column<bool>(type: "bit", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeDemandante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoDemandante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempoDeMercado = table.Column<int>(type: "int", nullable: false),
                    PorteDaEmpresa = table.Column<int>(type: "int", nullable: false),
                    RamoDeAtuacao = table.Column<int>(type: "int", nullable: false),
                    SegmentoDeMercado = table.Column<int>(type: "int", nullable: false),
                    LinhaDeAtuacaoTI = table.Column<int>(type: "int", nullable: false),
                    RegimeDeTributacao = table.Column<int>(type: "int", nullable: false),
                    LeiDeInformatica = table.Column<int>(type: "int", nullable: false),
                    ObjetivoParceria = table.Column<int>(type: "int", nullable: false),
                    AreaSolucaoBuscada = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demandas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solucionadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Formacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaDePesquisa = table.Column<int>(type: "int", nullable: false),
                    CurriculoLattes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiniBio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solucionadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DemandasSolucionadores",
                columns: table => new
                {
                    DemandaId = table.Column<int>(type: "int", nullable: false),
                    SolucionadorId = table.Column<int>(type: "int", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandasSolucionadores");

            migrationBuilder.DropTable(
                name: "Demandas");

            migrationBuilder.DropTable(
                name: "Solucionadores");
        }
    }
}
