using Microsoft.EntityFrameworkCore.Migrations;

namespace EventoTec.Web.Migrations
{
    public partial class CreateeCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_events_EventId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_EventId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_events_CategoryId",
                table: "events",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_Categories_CategoryId",
                table: "events",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_Categories_CategoryId",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_CategoryId",
                table: "events");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "events");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Categories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EventId",
                table: "Categories",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_events_EventId",
                table: "Categories",
                column: "EventId",
                principalTable: "events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
