using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMD_TechX.Migrations
{
    /// <inheritdoc />
    public partial class cambioCalendario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Viernes",
                table: "Calendario",
                newName: "viernes");

            migrationBuilder.RenameColumn(
                name: "Martes",
                table: "Calendario",
                newName: "martes");

            migrationBuilder.RenameColumn(
                name: "Lunes",
                table: "Calendario",
                newName: "lunes");

            migrationBuilder.RenameColumn(
                name: "Jueves",
                table: "Calendario",
                newName: "jueves");

            migrationBuilder.RenameColumn(
                name: "Domingo",
                table: "Calendario",
                newName: "domingo");

            migrationBuilder.RenameColumn(
                name: "Sabado",
                table: "Calendario",
                newName: "sábado");

            migrationBuilder.RenameColumn(
                name: "Miercoles",
                table: "Calendario",
                newName: "miércoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "viernes",
                table: "Calendario",
                newName: "Viernes");

            migrationBuilder.RenameColumn(
                name: "martes",
                table: "Calendario",
                newName: "Martes");

            migrationBuilder.RenameColumn(
                name: "lunes",
                table: "Calendario",
                newName: "Lunes");

            migrationBuilder.RenameColumn(
                name: "jueves",
                table: "Calendario",
                newName: "Jueves");

            migrationBuilder.RenameColumn(
                name: "domingo",
                table: "Calendario",
                newName: "Domingo");

            migrationBuilder.RenameColumn(
                name: "sábado",
                table: "Calendario",
                newName: "Sabado");

            migrationBuilder.RenameColumn(
                name: "miércoles",
                table: "Calendario",
                newName: "Miercoles");
        }
    }
}
