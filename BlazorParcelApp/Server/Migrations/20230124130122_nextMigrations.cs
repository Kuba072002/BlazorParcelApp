using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorParcelApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class nextMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Parcels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Lockers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Lockers");
        }
    }
}
