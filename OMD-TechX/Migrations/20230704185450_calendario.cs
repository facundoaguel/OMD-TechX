using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMD_TechX.Migrations
{
    /// <inheritdoc />
    public partial class calendario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lunes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Martes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miercoles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jueves = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Viernes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sabado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domingo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendario", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendario");
        }
    }
}
