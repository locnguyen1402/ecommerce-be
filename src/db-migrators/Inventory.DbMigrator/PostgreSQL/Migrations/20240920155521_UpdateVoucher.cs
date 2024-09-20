using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "merchant_id",
                table: "vouchers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "vouchers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "'UNSPECIFIED'");

            migrationBuilder.CreateIndex(
                name: "ix_vouchers_merchant_id",
                table: "vouchers",
                column: "merchant_id");

            migrationBuilder.AddForeignKey(
                name: "fk_vouchers_merchants_merchant_id",
                table: "vouchers",
                column: "merchant_id",
                principalTable: "merchants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vouchers_merchants_merchant_id",
                table: "vouchers");

            migrationBuilder.DropIndex(
                name: "ix_vouchers_merchant_id",
                table: "vouchers");

            migrationBuilder.DropColumn(
                name: "merchant_id",
                table: "vouchers");

            migrationBuilder.DropColumn(
                name: "status",
                table: "vouchers");
        }
    }
}
