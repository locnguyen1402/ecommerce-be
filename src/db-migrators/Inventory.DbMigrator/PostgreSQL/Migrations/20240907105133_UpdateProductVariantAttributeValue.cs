using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductVariantAttributeValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "attribute_value_id",
                table: "product_variant_attribute_values",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "product_attributes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "predefined",
                table: "product_attributes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "attribute_values",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    value = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    product_attribute_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attribute_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_attribute_values_product_attributes_product_attribute_id",
                        column: x => x.product_attribute_id,
                        principalTable: "product_attributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_variant_attribute_values_attribute_value_id",
                table: "product_variant_attribute_values",
                column: "attribute_value_id");

            migrationBuilder.CreateIndex(
                name: "ix_attribute_values_product_attribute_id",
                table: "attribute_values",
                column: "product_attribute_id");

            migrationBuilder.AddForeignKey(
                name: "fk_product_variant_attribute_values_attribute_values_attribute",
                table: "product_variant_attribute_values",
                column: "attribute_value_id",
                principalTable: "attribute_values",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_product_variant_attribute_values_attribute_values_attribute",
                table: "product_variant_attribute_values");

            migrationBuilder.DropTable(
                name: "attribute_values");

            migrationBuilder.DropIndex(
                name: "ix_product_variant_attribute_values_attribute_value_id",
                table: "product_variant_attribute_values");

            migrationBuilder.DropColumn(
                name: "attribute_value_id",
                table: "product_variant_attribute_values");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "product_attributes");

            migrationBuilder.DropColumn(
                name: "predefined",
                table: "product_attributes");
        }
    }
}
