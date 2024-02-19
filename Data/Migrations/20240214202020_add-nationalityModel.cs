using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookify.Data.Migrations
{
    /// <inheritdoc />
    public partial class addnationalityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "authors");

            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                table: "authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Nationality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationality", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Nationality",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Afghan" },
                    { 2, "Albanian" },
                    { 3, "Algerian" },
                    { 4, "American" },
                    { 5, "Andorran" },
                    { 6, "Angolan" },
                    { 7, "Antiguans" },
                    { 8, "Argentinean" },
                    { 9, "Armenian" },
                    { 10, "Australian" },
                    { 11, "Austrian" },
                    { 12, "Azerbaijani" },
                    { 13, "Bahamian" },
                    { 14, "Bahraini" },
                    { 15, "Bangladeshi" },
                    { 16, "Barbadian" },
                    { 17, "Barbudans" },
                    { 18, "Batswana" },
                    { 19, "Belarusian" },
                    { 20, "Belgian" },
                    { 21, "Belizean" },
                    { 22, "Beninese" },
                    { 23, "Bhutanese" },
                    { 24, "Bolivian" },
                    { 25, "Bosnian" },
                    { 26, "Brazilian" },
                    { 27, "British" },
                    { 28, "Bruneian" },
                    { 29, "Bulgarian" },
                    { 30, "Burkinabe" },
                    { 31, "Burmese" },
                    { 32, "Burundian" },
                    { 33, "Cambodian" },
                    { 34, "Cameroonian" },
                    { 35, "Canadian" },
                    { 36, "Cape Verdean" },
                    { 37, "Central African" },
                    { 38, "Chadian" },
                    { 39, "Chilean" },
                    { 40, "Chinese" },
                    { 41, "Colombian" },
                    { 42, "Comoran" },
                    { 43, "Congolese" },
                    { 44, "Costa Rican" },
                    { 45, "Croatian" },
                    { 46, "Cuban" },
                    { 47, "Cypriot" },
                    { 48, "Czech" },
                    { 49, "Danish" },
                    { 50, "Djibouti" },
                    { 51, "Dominican" },
                    { 52, "Dutch" },
                    { 53, "East Timorese" },
                    { 54, "Ecuadorean" },
                    { 55, "Egyptian" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_authors_NationalityId",
                table: "authors",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_authors_Nationality_NationalityId",
                table: "authors",
                column: "NationalityId",
                principalTable: "Nationality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_authors_Nationality_NationalityId",
                table: "authors");

            migrationBuilder.DropTable(
                name: "Nationality");

            migrationBuilder.DropIndex(
                name: "IX_authors_NationalityId",
                table: "authors");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "authors");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "authors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
