using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMD_TechX.Migrations
{
    /// <inheritdoc />
    public partial class precioAtencion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "Atenciones",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Atenciones");
        }
    }
}
