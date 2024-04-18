using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanner.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAttendeeFieldInReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventReservations_AspNetUsers_AttendeeId",
                table: "EventReservations");

            migrationBuilder.DropIndex(
                name: "IX_EventReservations_AttendeeId",
                table: "EventReservations");

            migrationBuilder.DropColumn(
                name: "AttendeeId",
                table: "EventReservations");

            migrationBuilder.AddColumn<string>(
                name: "AttendeeEmail",
                table: "EventReservations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendeeEmail",
                table: "EventReservations");

            migrationBuilder.AddColumn<string>(
                name: "AttendeeId",
                table: "EventReservations",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EventReservations_AttendeeId",
                table: "EventReservations",
                column: "AttendeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventReservations_AspNetUsers_AttendeeId",
                table: "EventReservations",
                column: "AttendeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
