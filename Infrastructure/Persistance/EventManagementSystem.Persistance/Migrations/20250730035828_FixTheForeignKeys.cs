using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementSystem.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FixTheForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Users_PublicUserId",
                table: "EventRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_PublicUserId",
                table: "EventRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "PublicUserId",
                table: "EventRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicUserId",
                table: "EventRegistrations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_PublicUserId",
                table: "EventRegistrations",
                column: "PublicUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Users_PublicUserId",
                table: "EventRegistrations",
                column: "PublicUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
