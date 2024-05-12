using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanner.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedEventPriceCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceCurrency",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "Events");
        }
    }
}
