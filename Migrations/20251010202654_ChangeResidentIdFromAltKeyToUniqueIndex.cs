using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_SC_Project.Migrations
{
    /// <inheritdoc />
    public partial class ChangeResidentIdFromAltKeyToUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Residents_ResidentId",
                table: "Residents");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_ResidentId",
                table: "Residents",
                column: "ResidentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Residents_ResidentId",
                table: "Residents");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Residents_ResidentId",
                table: "Residents",
                column: "ResidentId");
        }
    }
}
