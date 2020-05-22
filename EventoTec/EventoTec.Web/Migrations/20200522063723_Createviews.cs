using Microsoft.EntityFrameworkCore.Migrations;

namespace EventoTec.Web.Migrations
{
    public partial class Createviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_events_ClientId",
                table: "events",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_events_Clients_ClientId",
                table: "events",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_Clients_ClientId",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_ClientId",
                table: "events");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "events");
        }
    }
}
