using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeShopCollectionProductRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_shop_collections_shop_collection_id",
                table: "products");

            migrationBuilder.DropTable(
                name: "merchant_products");

            migrationBuilder.DropIndex(
                name: "ix_products_shop_collection_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "shop_collection_id",
                table: "products");

            migrationBuilder.CreateTable(
                name: "shop_collection_product",
                columns: table => new
                {
                    shop_collection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shop_collection_product", x => new { x.shop_collection_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_shop_collection_product_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_shop_collection_product_shop_collections_shop_collection_id",
                        column: x => x.shop_collection_id,
                        principalTable: "shop_collections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_shop_collection_product_product_id",
                table: "shop_collection_product",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shop_collection_product");

            migrationBuilder.AddColumn<Guid>(
                name: "shop_collection_id",
                table: "products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "merchant_products",
                columns: table => new
                {
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merchant_products", x => new { x.merchant_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_merchant_products_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_merchant_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_products_shop_collection_id",
                table: "products",
                column: "shop_collection_id");

            migrationBuilder.CreateIndex(
                name: "ix_merchant_products_product_id",
                table: "merchant_products",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_shop_collections_shop_collection_id",
                table: "products",
                column: "shop_collection_id",
                principalTable: "shop_collections",
                principalColumn: "id");
        }
    }
}
