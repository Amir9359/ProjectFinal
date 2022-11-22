using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CatelogMigration_up2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 2, 9, 47, 39, 320, DateTimeKind.Local).AddTicks(5085),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 1, 20, 37, 7, 702, DateTimeKind.Local).AddTicks(2204));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 2, 9, 47, 39, 290, DateTimeKind.Local).AddTicks(5268),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 1, 20, 37, 7, 689, DateTimeKind.Local).AddTicks(7378));

            migrationBuilder.InsertData(
                table: "CatalogType",
                columns: new[] { "Id", "EditTime", "ParentCatalogTypeId", "RemoveTime", "Type" },
                values: new object[] { 1, null, null, null, "کالای دیجیتال" });

            migrationBuilder.InsertData(
                table: "CatalogType",
                columns: new[] { "Id", "EditTime", "ParentCatalogTypeId", "RemoveTime", "Type" },
                values: new object[] { 2, null, 1, null, "لوازم جانبی گوشی" });

            migrationBuilder.InsertData(
                table: "CatalogType",
                columns: new[] { "Id", "EditTime", "ParentCatalogTypeId", "RemoveTime", "Type" },
                values: new object[] { 3, null, 2, null, "پایه نگهدارنده گوشی" });

            migrationBuilder.InsertData(
                table: "CatalogType",
                columns: new[] { "Id", "EditTime", "ParentCatalogTypeId", "RemoveTime", "Type" },
                values: new object[] { 4, null, 2, null, "پاور بانک (شارژر همراه)" });

            migrationBuilder.InsertData(
                table: "CatalogType",
                columns: new[] { "Id", "EditTime", "ParentCatalogTypeId", "RemoveTime", "Type" },
                values: new object[] { 5, null, 2, null, "کیف و کاور گوشی" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 1, 20, 37, 7, 702, DateTimeKind.Local).AddTicks(2204),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 2, 9, 47, 39, 320, DateTimeKind.Local).AddTicks(5085));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 1, 20, 37, 7, 689, DateTimeKind.Local).AddTicks(7378),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 2, 9, 47, 39, 290, DateTimeKind.Local).AddTicks(5268));
        }
    }
}
