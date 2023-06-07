using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMD_TechX.Migrations
{
    /// <inheritdoc />
    public partial class perroAtencionString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerroAtenciones_Atenciones_AtencionId",
                table: "PerroAtenciones");

            migrationBuilder.DropIndex(
                name: "IX_PerroAtenciones_AtencionId",
                table: "PerroAtenciones");

            migrationBuilder.AddColumn<string>(
                name: "Atencion",
                table: "PerroAtenciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "perro",
                table: "PerroAtenciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Atencion",
                table: "PerroAtenciones");

            migrationBuilder.DropColumn(
                name: "perro",
                table: "PerroAtenciones");

            migrationBuilder.CreateIndex(
                name: "IX_PerroAtenciones_AtencionId",
                table: "PerroAtenciones",
                column: "AtencionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerroAtenciones_Atenciones_AtencionId",
                table: "PerroAtenciones",
                column: "AtencionId",
                principalTable: "Atenciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
