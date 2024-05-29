using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_Id",
                table: "Reservations",
                column: "Id",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_Id",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
