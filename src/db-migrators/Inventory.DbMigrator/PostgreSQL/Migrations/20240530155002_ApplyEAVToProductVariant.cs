using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class ApplyEAVToProductVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "products");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "product_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_attributes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_variants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    stock = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    price = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_variants", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_variants_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_variant_attribute_values",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_attribute_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_variant_attribute_values", x => x.id);
                    table.UniqueConstraint("ak_product_variant_attribute_values_product_variant_id_product", x => new { x.product_variant_id, x.product_attribute_id });
                    table.ForeignKey(
                        name: "fk_product_variant_attribute_values_product_attributes_product",
                        column: x => x.product_attribute_id,
                        principalTable: "product_attributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_variant_attribute_values_product_variants_product_v",
                        column: x => x.product_variant_id,
                        principalTable: "product_variants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_variant_attribute_values_product_attribute_id",
                table: "product_variant_attribute_values",
                column: "product_attribute_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_variants_product_id",
                table: "product_variants",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_variant_attribute_values");

            migrationBuilder.DropTable(
                name: "product_attributes");

            migrationBuilder.DropTable(
                name: "product_variants");

            migrationBuilder.DropColumn(
                name: "description",
                table: "products");

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
