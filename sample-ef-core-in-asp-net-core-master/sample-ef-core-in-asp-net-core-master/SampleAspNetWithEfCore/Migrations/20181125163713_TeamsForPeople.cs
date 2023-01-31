using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleAspNetWithEfCore.Migrations
{
    public partial class TeamsForPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "People",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Code",
                table: "Teams",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_People_TeamId",
                table: "People",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Teams_TeamId",
                table: "People",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Teams_TeamId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Teams_Code",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_People_TeamId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "People");
        }
    }
}
