using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMD_TechX.Migrations
{
    /// <inheritdoc />
    public partial class ryd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Refugios_RefugioId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_RefugioId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "RefugioId",
                table: "Usuarios");

            migrationBuilder.CreateTable(
                name: "Donaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Donado = table.Column<double>(type: "float", nullable: false),
                    RefugioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donaciones", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donaciones");

            migrationBuilder.AddColumn<int>(
                name: "RefugioId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RefugioId",
                table: "Usuarios",
                column: "RefugioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Refugios_RefugioId",
                table: "Usuarios",
                column: "RefugioId",
                principalTable: "Refugios",
                principalColumn: "Id");
        }
    }
}
