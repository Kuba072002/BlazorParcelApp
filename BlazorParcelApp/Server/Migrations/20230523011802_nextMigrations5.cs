using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorParcelApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class nextMigrations5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Parcels_ParcelId",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "ParcelId",
                table: "Notifications",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Parcels_ParcelId",
                table: "Notifications",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Parcels_ParcelId",
                table: "Notifications");

            migrationBuilder.AlterColumn<int>(
                name: "ParcelId",
                table: "Notifications",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Parcels_ParcelId",
                table: "Notifications",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
