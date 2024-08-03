using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVillaAgency.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_relation_table_basket_coupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndedDate",
                table: "Coupons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "Baskets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_CouponId",
                table: "Baskets",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Coupons_CouponId",
                table: "Baskets",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "CouponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Coupons_CouponId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_CouponId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Baskets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndedDate",
                table: "Coupons",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
