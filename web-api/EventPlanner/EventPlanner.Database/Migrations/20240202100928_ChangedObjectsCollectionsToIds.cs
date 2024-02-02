using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanner.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangedObjectsCollectionsToIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventReservations_Events_EventId",
                table: "EventReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_LocationId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_EventReservations_EventId",
                table: "EventReservations");

            migrationBuilder.AddColumn<string>(
                name: "EventsId",
                table: "Locations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ReservationsId",
                table: "Events",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventsId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "ReservationsId",
                table: "Events");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReservations_EventId",
                table: "EventReservations",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReservations_Events_EventId",
                table: "EventReservations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
