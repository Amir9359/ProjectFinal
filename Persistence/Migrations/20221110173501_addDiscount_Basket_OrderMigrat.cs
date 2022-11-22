using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class addDiscount_Basket_OrderMigrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 330, DateTimeKind.Local).AddTicks(2129),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 454, DateTimeKind.Local).AddTicks(3068));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 330, DateTimeKind.Local).AddTicks(136),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 453, DateTimeKind.Local).AddTicks(9579));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 329, DateTimeKind.Local).AddTicks(4405),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 453, DateTimeKind.Local).AddTicks(771));

            migrationBuilder.AddColumn<int>(
                name: "AppliedDiscountId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 329, DateTimeKind.Local).AddTicks(7821),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 453, DateTimeKind.Local).AddTicks(6046));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 329, DateTimeKind.Local).AddTicks(609),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 452, DateTimeKind.Local).AddTicks(6594));

            migrationBuilder.AlterColumn<int>(
                name: "DiscountPersentage",
                table: "Discounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountAmount",
                table: "Discounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 328, DateTimeKind.Local).AddTicks(7684),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 452, DateTimeKind.Local).AddTicks(1986));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 327, DateTimeKind.Local).AddTicks(9969),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 450, DateTimeKind.Local).AddTicks(7797));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 328, DateTimeKind.Local).AddTicks(5619),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 451, DateTimeKind.Local).AddTicks(7245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 328, DateTimeKind.Local).AddTicks(3245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 451, DateTimeKind.Local).AddTicks(3732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 327, DateTimeKind.Local).AddTicks(6525),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 450, DateTimeKind.Local).AddTicks(3296));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 314, DateTimeKind.Local).AddTicks(45),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 431, DateTimeKind.Local).AddTicks(2374));

            migrationBuilder.AddColumn<int>(
                name: "AppliedDiscountId",
                table: "Baskets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountAmount",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 326, DateTimeKind.Local).AddTicks(8575),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 449, DateTimeKind.Local).AddTicks(9010));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppliedDiscountId",
                table: "Orders",
                column: "AppliedDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_AppliedDiscountId",
                table: "Baskets",
                column: "AppliedDiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Discounts_AppliedDiscountId",
                table: "Baskets",
                column: "AppliedDiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Discounts_AppliedDiscountId",
                table: "Orders",
                column: "AppliedDiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Discounts_AppliedDiscountId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Discounts_AppliedDiscountId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AppliedDiscountId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_AppliedDiscountId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "AppliedDiscountId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AppliedDiscountId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Baskets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 454, DateTimeKind.Local).AddTicks(3068),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 330, DateTimeKind.Local).AddTicks(2129));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 453, DateTimeKind.Local).AddTicks(9579),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 330, DateTimeKind.Local).AddTicks(136));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 453, DateTimeKind.Local).AddTicks(771),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 329, DateTimeKind.Local).AddTicks(4405));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 453, DateTimeKind.Local).AddTicks(6046),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 329, DateTimeKind.Local).AddTicks(7821));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 452, DateTimeKind.Local).AddTicks(6594),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 329, DateTimeKind.Local).AddTicks(609));

            migrationBuilder.AlterColumn<bool>(
                name: "DiscountPersentage",
                table: "Discounts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "DiscountAmount",
                table: "Discounts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 452, DateTimeKind.Local).AddTicks(1986),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 328, DateTimeKind.Local).AddTicks(7684));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 450, DateTimeKind.Local).AddTicks(7797),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 327, DateTimeKind.Local).AddTicks(9969));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 451, DateTimeKind.Local).AddTicks(7245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 328, DateTimeKind.Local).AddTicks(5619));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 451, DateTimeKind.Local).AddTicks(3732),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 328, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 450, DateTimeKind.Local).AddTicks(3296),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 327, DateTimeKind.Local).AddTicks(6525));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 431, DateTimeKind.Local).AddTicks(2374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 314, DateTimeKind.Local).AddTicks(45));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 11, 9, 20, 29, 27, 449, DateTimeKind.Local).AddTicks(9010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 11, 10, 21, 5, 1, 326, DateTimeKind.Local).AddTicks(8575));
        }
    }
}
