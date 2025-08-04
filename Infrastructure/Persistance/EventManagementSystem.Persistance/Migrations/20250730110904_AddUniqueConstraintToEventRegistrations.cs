using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementSystem.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToEventRegistrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicUserId",
                table: "EventRegistrations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_PublicUserId_EventId",
                table: "EventRegistrations",
                columns: new[] { "PublicUserId", "EventId" },
                unique: true,
                filter: "[EventId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventRegistrations_PublicUserId_EventId",
                table: "EventRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "PublicUserId",
                table: "EventRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
