using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMD_TechX.Migrations
{
    /// <inheritdoc />
    public partial class publicacion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Publicacion",
                table: "Publicacion");

            migrationBuilder.RenameTable(
                name: "Publicacion",
                newName: "Publicaciones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publicaciones",
                table: "Publicaciones",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Publicaciones",
                table: "Publicaciones");

            migrationBuilder.RenameTable(
                name: "Publicaciones",
                newName: "Publicacion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publicacion",
                table: "Publicacion",
                column: "Id");
        }
    }
}
