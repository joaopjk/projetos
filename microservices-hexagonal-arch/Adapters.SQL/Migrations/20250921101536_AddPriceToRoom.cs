using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adapters.SQL.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Rooms");
        }
    }
}
