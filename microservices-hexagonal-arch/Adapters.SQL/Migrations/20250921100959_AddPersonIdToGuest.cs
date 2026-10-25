using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adapters.SQL.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonIdToGuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentNumber",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "Guests",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentNumber",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Guests");
        }
    }
}
