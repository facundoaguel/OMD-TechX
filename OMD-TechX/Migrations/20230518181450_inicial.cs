using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMD_TechX.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerroId",
                table: "Turnos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_PerroId",
                table: "Turnos",
                column: "PerroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turnos_Perros_PerroId",
                table: "Turnos",
                column: "PerroId",
                principalTable: "Perros",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turnos_Perros_PerroId",
                table: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Turnos_PerroId",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "PerroId",
                table: "Turnos");
        }
    }
}
