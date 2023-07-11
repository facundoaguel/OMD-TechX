using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMD_TechX.Migrations
{
    /// <inheritdoc />
    public partial class cambioRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnCampania",
                table: "Refugios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnCampania",
                table: "Refugios");
        }
    }
}
