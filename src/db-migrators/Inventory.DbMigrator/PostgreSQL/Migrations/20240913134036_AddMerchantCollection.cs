using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddMerchantCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "merchant_collections");

            migrationBuilder.DropIndex(
                name: "ix_merchant_products_product_id",
                table: "merchant_products");

            migrationBuilder.AddColumn<Guid>(
                name: "merchant_product_id",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "shop_collection_id",
                table: "merchant_products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "shop_collections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shop_collections", x => x.id);
                    table.ForeignKey(
                        name: "fk_shop_collections_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_shop_collections_shop_collections_parent_id",
                        column: x => x.parent_id,
                        principalTable: "shop_collections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_merchant_products_product_id",
                table: "merchant_products",
                column: "product_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_merchant_products_shop_collection_id",
                table: "merchant_products",
                column: "shop_collection_id");

            migrationBuilder.CreateIndex(
                name: "ix_shop_collections_merchant_id",
                table: "shop_collections",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "ix_shop_collections_parent_id",
                table: "shop_collections",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "fk_merchant_products_shop_collections_shop_collection_id",
                table: "merchant_products",
                column: "shop_collection_id",
                principalTable: "shop_collections",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_merchant_products_shop_collections_shop_collection_id",
                table: "merchant_products");

            migrationBuilder.DropTable(
                name: "shop_collections");

            migrationBuilder.DropIndex(
                name: "ix_merchant_products_product_id",
                table: "merchant_products");

            migrationBuilder.DropIndex(
                name: "ix_merchant_products_shop_collection_id",
                table: "merchant_products");

            migrationBuilder.DropColumn(
                name: "merchant_product_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "shop_collection_id",
                table: "merchant_products");

            migrationBuilder.CreateTable(
                name: "merchant_collections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merchant_collections", x => x.id);
                    table.ForeignKey(
                        name: "fk_merchant_collections_categories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_merchant_collections_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_merchant_products_product_id",
                table: "merchant_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_merchant_collections_merchant_id",
                table: "merchant_collections",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "ix_merchant_collections_parent_id",
                table: "merchant_collections",
                column: "parent_id");
        }
    }
}
