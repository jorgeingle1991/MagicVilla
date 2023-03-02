using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKetToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VillaNumberModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 12, 20, 7, 675, DateTimeKind.Local).AddTicks(2448));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 12, 20, 7, 675, DateTimeKind.Local).AddTicks(2493));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 12, 20, 7, 675, DateTimeKind.Local).AddTicks(2497));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 12, 20, 7, 675, DateTimeKind.Local).AddTicks(2500));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 12, 20, 7, 675, DateTimeKind.Local).AddTicks(2503));

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumberModel_VillaID",
                table: "VillaNumberModel",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumberModel_Villas_VillaID",
                table: "VillaNumberModel",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumberModel_Villas_VillaID",
                table: "VillaNumberModel");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumberModel_VillaID",
                table: "VillaNumberModel");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VillaNumberModel");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 9, 59, 2, 707, DateTimeKind.Local).AddTicks(9020));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 9, 59, 2, 707, DateTimeKind.Local).AddTicks(9060));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 9, 59, 2, 707, DateTimeKind.Local).AddTicks(9062));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 9, 59, 2, 707, DateTimeKind.Local).AddTicks(9064));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2023, 3, 2, 9, 59, 2, 707, DateTimeKind.Local).AddTicks(9066));
        }
    }
}
