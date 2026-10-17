using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MenuSemanal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPlatillosYMenuPlatillos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "MenusGlobales",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Platillos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platillos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuPlatillos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuGlobalId = table.Column<int>(type: "integer", nullable: false),
                    PlatilloId = table.Column<int>(type: "integer", nullable: false),
                    DiaSemana = table.Column<int>(type: "integer", nullable: false),
                    TipoComida = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPlatillos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuPlatillos_MenusGlobales_MenuGlobalId",
                        column: x => x.MenuGlobalId,
                        principalTable: "MenusGlobales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPlatillos_Platillos_PlatilloId",
                        column: x => x.PlatilloId,
                        principalTable: "Platillos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuPlatillos_MenuGlobalId",
                table: "MenuPlatillos",
                column: "MenuGlobalId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPlatillos_PlatilloId",
                table: "MenuPlatillos",
                column: "PlatilloId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuPlatillos");

            migrationBuilder.DropTable(
                name: "Platillos");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "MenusGlobales",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
