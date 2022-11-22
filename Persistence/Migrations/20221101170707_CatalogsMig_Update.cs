using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CatalogsMig_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 1, 20, 37, 7, 702, DateTimeKind.Local).AddTicks(2204),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 1, 18, 48, 41, 989, DateTimeKind.Local).AddTicks(2073));

            migrationBuilder.AddColumn<int>(
                name: "ParentCatalogTypeId",
                table: "CatalogType",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 1, 20, 37, 7, 689, DateTimeKind.Local).AddTicks(7378),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 1, 18, 48, 41, 958, DateTimeKind.Local).AddTicks(6265));

            migrationBuilder.CreateIndex(
                name: "IX_CatalogType_ParentCatalogTypeId",
                table: "CatalogType",
                column: "ParentCatalogTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogType_CatalogType_ParentCatalogTypeId",
                table: "CatalogType",
                column: "ParentCatalogTypeId",
                principalTable: "CatalogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogType_CatalogType_ParentCatalogTypeId",
                table: "CatalogType");

            migrationBuilder.DropIndex(
                name: "IX_CatalogType_ParentCatalogTypeId",
                table: "CatalogType");

            migrationBuilder.DropColumn(
                name: "ParentCatalogTypeId",
                table: "CatalogType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 1, 18, 48, 41, 989, DateTimeKind.Local).AddTicks(2073),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 1, 20, 37, 7, 702, DateTimeKind.Local).AddTicks(2204));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 1, 18, 48, 41, 958, DateTimeKind.Local).AddTicks(6265),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 1, 20, 37, 7, 689, DateTimeKind.Local).AddTicks(7378));
        }
    }
}
