using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepotId",
                table: "Productions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productions_DepotId",
                table: "Productions",
                column: "DepotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productions_Depots_DepotId",
                table: "Productions",
                column: "DepotId",
                principalTable: "Depots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productions_Depots_DepotId",
                table: "Productions");

            migrationBuilder.DropIndex(
                name: "IX_Productions_DepotId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "DepotId",
                table: "Productions");
        }
    }
}
