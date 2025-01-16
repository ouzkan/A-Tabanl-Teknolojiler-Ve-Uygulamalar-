using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country_Names",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_Names", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country_Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Images_Country_Names_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country_Names",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Images_CountryId",
                table: "Country_Images",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country_Images");

            migrationBuilder.DropTable(
                name: "Country_Names");
        }
    }
}
