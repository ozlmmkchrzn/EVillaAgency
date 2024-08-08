using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVillaAgency.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_identity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AppUserId",
                table: "Favorites",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AppUser_AppUserId",
                table: "Favorites",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AppUser_AppUserId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_AppUserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Favorites");
        }
    }
}
