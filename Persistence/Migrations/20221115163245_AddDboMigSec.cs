using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddDboMigSec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "UserAddresses",
                newName: "UserAddresses",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payments",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Orders",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItems",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "DiscountUsageHistories",
                newName: "DiscountUsageHistories",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "Discounts",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CatalogType",
                newName: "CatalogType",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CatalogItems",
                newName: "CatalogItems",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CatalogItemImage",
                newName: "CatalogItemImage",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CatalogItemFeature",
                newName: "CatalogItemFeature",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CatalogItemFavourites",
                newName: "CatalogItemFavourites",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CatalogItemDiscount",
                newName: "CatalogItemDiscount",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CatalogItemComments",
                newName: "CatalogItemComments",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "CatalogBrand",
                newName: "CatalogBrand",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Baskets",
                newName: "Baskets",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "BasketItems",
                newName: "BasketItems",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Banners",
                newName: "Banners",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 732, DateTimeKind.Local).AddTicks(3299),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 777, DateTimeKind.Local).AddTicks(8111));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 731, DateTimeKind.Local).AddTicks(9848),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 777, DateTimeKind.Local).AddTicks(6516));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 730, DateTimeKind.Local).AddTicks(8374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 777, DateTimeKind.Local).AddTicks(1623));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 731, DateTimeKind.Local).AddTicks(5437),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 777, DateTimeKind.Local).AddTicks(4699));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 730, DateTimeKind.Local).AddTicks(1341),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(8535));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 729, DateTimeKind.Local).AddTicks(6092),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(6090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 727, DateTimeKind.Local).AddTicks(7260),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 775, DateTimeKind.Local).AddTicks(8071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 729, DateTimeKind.Local).AddTicks(1706),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(4458));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 728, DateTimeKind.Local).AddTicks(7947),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(2823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "CatalogItemFavourites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 728, DateTimeKind.Local).AddTicks(4545),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(1380));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "CatalogItemComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 704, DateTimeKind.Local).AddTicks(9985),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 765, DateTimeKind.Local).AddTicks(8259));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 727, DateTimeKind.Local).AddTicks(2210),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 775, DateTimeKind.Local).AddTicks(5794));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 726, DateTimeKind.Local).AddTicks(3440),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 775, DateTimeKind.Local).AddTicks(1994));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                schema: "dbo",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 726, DateTimeKind.Local).AddTicks(8165),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 775, DateTimeKind.Local).AddTicks(3937));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "UserAddresses",
                schema: "dbo",
                newName: "UserAddresses");

            migrationBuilder.RenameTable(
                name: "Payments",
                schema: "dbo",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "dbo",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                schema: "dbo",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "DiscountUsageHistories",
                schema: "dbo",
                newName: "DiscountUsageHistories");

            migrationBuilder.RenameTable(
                name: "Discounts",
                schema: "dbo",
                newName: "Discounts");

            migrationBuilder.RenameTable(
                name: "CatalogType",
                schema: "dbo",
                newName: "CatalogType");

            migrationBuilder.RenameTable(
                name: "CatalogItems",
                schema: "dbo",
                newName: "CatalogItems");

            migrationBuilder.RenameTable(
                name: "CatalogItemImage",
                schema: "dbo",
                newName: "CatalogItemImage");

            migrationBuilder.RenameTable(
                name: "CatalogItemFeature",
                schema: "dbo",
                newName: "CatalogItemFeature");

            migrationBuilder.RenameTable(
                name: "CatalogItemFavourites",
                schema: "dbo",
                newName: "CatalogItemFavourites");

            migrationBuilder.RenameTable(
                name: "CatalogItemDiscount",
                schema: "dbo",
                newName: "CatalogItemDiscount");

            migrationBuilder.RenameTable(
                name: "CatalogItemComments",
                schema: "dbo",
                newName: "CatalogItemComments");

            migrationBuilder.RenameTable(
                name: "CatalogBrand",
                schema: "dbo",
                newName: "CatalogBrand");

            migrationBuilder.RenameTable(
                name: "Baskets",
                schema: "dbo",
                newName: "Baskets");

            migrationBuilder.RenameTable(
                name: "BasketItems",
                schema: "dbo",
                newName: "BasketItems");

            migrationBuilder.RenameTable(
                name: "Banners",
                schema: "dbo",
                newName: "Banners");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 777, DateTimeKind.Local).AddTicks(8111),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 732, DateTimeKind.Local).AddTicks(3299));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 777, DateTimeKind.Local).AddTicks(6516),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 731, DateTimeKind.Local).AddTicks(9848));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 777, DateTimeKind.Local).AddTicks(1623),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 730, DateTimeKind.Local).AddTicks(8374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 777, DateTimeKind.Local).AddTicks(4699),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 731, DateTimeKind.Local).AddTicks(5437));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(8535),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 730, DateTimeKind.Local).AddTicks(1341));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(6090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 729, DateTimeKind.Local).AddTicks(6092));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 775, DateTimeKind.Local).AddTicks(8071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 727, DateTimeKind.Local).AddTicks(7260));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(4458),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 729, DateTimeKind.Local).AddTicks(1706));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(2823),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 728, DateTimeKind.Local).AddTicks(7947));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFavourites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 776, DateTimeKind.Local).AddTicks(1380),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 728, DateTimeKind.Local).AddTicks(4545));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 765, DateTimeKind.Local).AddTicks(8259),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 704, DateTimeKind.Local).AddTicks(9985));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 775, DateTimeKind.Local).AddTicks(5794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 727, DateTimeKind.Local).AddTicks(2210));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 775, DateTimeKind.Local).AddTicks(1994),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 726, DateTimeKind.Local).AddTicks(3440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 15, 19, 59, 58, 775, DateTimeKind.Local).AddTicks(3937),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 15, 20, 2, 44, 726, DateTimeKind.Local).AddTicks(8165));
        }
    }
}
