using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMD_TechX.Migrations
{
    /// <inheritdoc />
    public partial class cambioDesconocido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tamanio",
                table: "Perros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tamanio",
                table: "Perros");
        }
    }
}
