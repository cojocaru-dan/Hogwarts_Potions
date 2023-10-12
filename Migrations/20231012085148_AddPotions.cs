using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogwartsPotions.Migrations
{
    /// <inheritdoc />
    public partial class AddPotions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PotionID",
                table: "Ingredients",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Potions",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentID = table.Column<long>(type: "bigint", nullable: true),
                    BrewingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Potions_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Potions_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_PotionID",
                table: "Ingredients",
                column: "PotionID");

            migrationBuilder.CreateIndex(
                name: "IX_Potions_RecipeID",
                table: "Potions",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Potions_StudentID",
                table: "Potions",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Potions_PotionID",
                table: "Ingredients",
                column: "PotionID",
                principalTable: "Potions",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Potions_PotionID",
                table: "Ingredients");

            migrationBuilder.DropTable(
                name: "Potions");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_PotionID",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "PotionID",
                table: "Ingredients");
        }
    }
}
