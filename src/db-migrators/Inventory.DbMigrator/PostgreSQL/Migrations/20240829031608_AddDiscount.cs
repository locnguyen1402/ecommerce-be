﻿using System;
using System.Collections.Generic;
using ECommerce.Inventory.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "has_discounts_applied",
                table: "products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "has_discounts_applied",
                table: "categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    discount_percentage = table.Column<decimal>(type: "numeric", nullable: true),
                    discount_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    max_discount_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    start_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    end_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    max_redemptions = table.Column<int>(type: "integer", nullable: true),
                    redemption_quantity = table.Column<int>(type: "integer", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    limitation_times = table.Column<int>(type: "integer", nullable: true),
                    limitation_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "'UNSPECIFIED'"),
                    discount_usage_history = table.Column<IReadOnlyCollection<DiscountUsageHistory>>(type: "jsonb", nullable: false, defaultValueSql: "'[]'"),
                    discount_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    has_discounts_applied = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_discounts_discounts_discount_id",
                        column: x => x.discount_id,
                        principalTable: "discounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "discount_applied_to_categories",
                columns: table => new
                {
                    discount_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discount_applied_to_categories", x => new { x.discount_id, x.category_id });
                    table.ForeignKey(
                        name: "fk_discount_applied_to_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_discount_applied_to_categories_discounts_discount_id",
                        column: x => x.discount_id,
                        principalTable: "discounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discount_applied_to_products",
                columns: table => new
                {
                    discount_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discount_applied_to_products", x => new { x.discount_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_discount_applied_to_products_discounts_discount_id",
                        column: x => x.discount_id,
                        principalTable: "discounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_discount_applied_to_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_discount_applied_to_categories_category_id",
                table: "discount_applied_to_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_applied_to_categories_discount_id",
                table: "discount_applied_to_categories",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_applied_to_products_discount_id",
                table: "discount_applied_to_products",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_discount_applied_to_products_product_id",
                table: "discount_applied_to_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_discounts_code",
                table: "discounts",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_discounts_discount_id",
                table: "discounts",
                column: "discount_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discount_applied_to_categories");

            migrationBuilder.DropTable(
                name: "discount_applied_to_products");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropColumn(
                name: "has_discounts_applied",
                table: "products");

            migrationBuilder.DropColumn(
                name: "has_discounts_applied",
                table: "categories");
        }
    }
}
