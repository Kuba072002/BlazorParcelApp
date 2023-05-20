using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorParcelApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class nextMigrations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Parcels");

            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "Parcels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "Parcels");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Parcels",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
