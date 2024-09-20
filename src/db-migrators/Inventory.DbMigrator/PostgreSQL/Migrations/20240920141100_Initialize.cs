using System;
using System.Collections.Generic;
using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.AggregatesModel.Common;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, defaultValueSql: "''"),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    has_discounts_applied = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_categories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    user_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    first_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    full_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "'UNSPECIFIED'"),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ref_user_id = table.Column<string>(type: "text", nullable: true),
                    level_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'SILVER'"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    discount_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    discount_value = table.Column<decimal>(type: "numeric", nullable: true),
                    discount_unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    min_order_value = table.Column<decimal>(type: "numeric", nullable: true),
                    max_discount_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    start_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    end_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
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
                name: "merchants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    merchant_number = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merchants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    predefined = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_attributes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vouchers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    code = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    start_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    applied_on_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    target_customer_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    popular_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    min_spend = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    max_quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    max_quantity_per_user = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    discount_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    value = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    max_value = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vouchers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    contact_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address_info = table.Column<AddressInfo>(type: "jsonb", nullable: false, defaultValueSql: "'{}'"),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contacts", x => x.id);
                    table.ForeignKey(
                        name: "fk_contacts_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "merchant_categories",
                columns: table => new
                {
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merchant_categories", x => new { x.merchant_id, x.category_id });
                    table.ForeignKey(
                        name: "fk_merchant_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_merchant_categories_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_promotions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    start_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'NEW'"),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    max_quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    min_spend = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    bundle_promotion_discount_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    conditions = table.Column<IReadOnlyCollection<OrderPromotionCondition>>(type: "jsonb", nullable: false, defaultValueSql: "'[]'"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_promotions", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_promotions_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    order_number = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'TO_PAY'"),
                    payment_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNPAID'"),
                    paid_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    payment_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    total_item_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    vat_percent = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    vat_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    delivery_fee = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    delivery_schedule = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_promotions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    start_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'NEW'"),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_promotions", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_promotions_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    has_discounts_applied = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    store_number = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    store_address = table.Column<string>(type: "text", nullable: true),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stores", x => x.id);
                    table.ForeignKey(
                        name: "fk_stores_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "order_contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    contact_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    address_info = table.Column<AddressInfo>(type: "jsonb", nullable: false, defaultValueSql: "'{}'"),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_contacts", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_contacts_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_status_trackings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_status_trackings", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_status_trackings_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment_method_trackings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    payment_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    value = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_method_trackings", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_method_trackings_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
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

            migrationBuilder.CreateTable(
                name: "order_promotion_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    order_promotion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_promotion_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_promotion_items_order_promotions_order_promotion_id",
                        column: x => x.order_promotion_id,
                        principalTable: "order_promotions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_promotion_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_product_attributes",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_attribute_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_product_attributes", x => new { x.product_id, x.product_attribute_id });
                    table.ForeignKey(
                        name: "fk_product_product_attributes_product_attributes_product_attri",
                        column: x => x.product_attribute_id,
                        principalTable: "product_attributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_product_attributes_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_variants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    stock = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
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
                name: "voucher_product",
                columns: table => new
                {
                    voucher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_voucher_product", x => new { x.voucher_id, x.product_id });
                    table.ForeignKey(
                        name: "fk_voucher_product_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_voucher_product_vouchers_voucher_id",
                        column: x => x.voucher_id,
                        principalTable: "vouchers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: true),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    product_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    product_description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    list_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    vat_percent = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: false),
                    vat_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    total_vat_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_items_product_variants_product_variant_id",
                        column: x => x.product_variant_id,
                        principalTable: "product_variants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_promotion_sub_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    order_promotion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    discount_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    discount_percentage = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    no_products_per_order_limit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'SPECIFIC'"),
                    max_items_per_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_promotion_sub_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_promotion_sub_items_order_promotions_order_promotion_",
                        column: x => x.order_promotion_id,
                        principalTable: "order_promotions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_promotion_sub_items_product_variants_product_variant_",
                        column: x => x.product_variant_id,
                        principalTable: "product_variants",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_order_promotion_sub_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_promotion_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    product_promotion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    list_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    discount_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    discount_percentage = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    no_products_per_order_limit = table.Column<int>(type: "integer", nullable: false),
                    max_items_per_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_promotion_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_promotion_items_product_promotions_product_promotio",
                        column: x => x.product_promotion_id,
                        principalTable: "product_promotions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_promotion_items_product_variants_product_variant_id",
                        column: x => x.product_variant_id,
                        principalTable: "product_variants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_promotion_items_products_product_id",
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
                    attribute_value_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_variant_attribute_values", x => x.id);
                    table.UniqueConstraint("ak_product_variant_attribute_values_product_variant_id_product", x => new { x.product_variant_id, x.product_attribute_id });
                    table.ForeignKey(
                        name: "fk_product_variant_attribute_values_attribute_values_attribute",
                        column: x => x.attribute_value_id,
                        principalTable: "attribute_values",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ix_attribute_values_product_attribute_id",
                table: "attribute_values",
                column: "product_attribute_id");

            migrationBuilder.CreateIndex(
                name: "ix_categories_parent_id",
                table: "categories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_contacts_customer_id",
                table: "contacts",
                column: "customer_id");

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
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_discounts_discount_id",
                table: "discounts",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "ix_merchant_categories_category_id",
                table: "merchant_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_contacts_order_id",
                table: "order_contacts",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_product_variant_id",
                table: "order_items",
                column: "product_variant_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_promotion_items_order_promotion_id",
                table: "order_promotion_items",
                column: "order_promotion_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_promotion_items_product_id",
                table: "order_promotion_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_promotion_sub_items_order_promotion_id",
                table: "order_promotion_sub_items",
                column: "order_promotion_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_promotion_sub_items_product_id",
                table: "order_promotion_sub_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_promotion_sub_items_product_variant_id",
                table: "order_promotion_sub_items",
                column: "product_variant_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_promotions_merchant_id",
                table: "order_promotions",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_status_trackings_order_id",
                table: "order_status_trackings",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_merchant_id",
                table: "orders",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_order_number",
                table: "orders",
                column: "order_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_payment_method_trackings_order_id",
                table: "payment_method_trackings",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_product_attributes_product_attribute_id",
                table: "product_product_attributes",
                column: "product_attribute_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_promotion_items_product_id",
                table: "product_promotion_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_promotion_items_product_promotion_id",
                table: "product_promotion_items",
                column: "product_promotion_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_promotion_items_product_variant_id",
                table: "product_promotion_items",
                column: "product_variant_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_promotions_merchant_id",
                table: "product_promotions",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_variant_attribute_values_attribute_value_id",
                table: "product_variant_attribute_values",
                column: "attribute_value_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_variant_attribute_values_product_attribute_id",
                table: "product_variant_attribute_values",
                column: "product_attribute_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_variants_product_id",
                table: "product_variants",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_products_merchant_id",
                table: "products",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "ix_shop_collection_product_product_id",
                table: "shop_collection_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_shop_collections_merchant_id",
                table: "shop_collections",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "ix_shop_collections_parent_id",
                table: "shop_collections",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_shop_collections_slug",
                table: "shop_collections",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stores_merchant_id",
                table: "stores",
                column: "merchant_id");

            migrationBuilder.CreateIndex(
                name: "ix_voucher_product_product_id",
                table: "voucher_product",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "discount_applied_to_categories");

            migrationBuilder.DropTable(
                name: "discount_applied_to_products");

            migrationBuilder.DropTable(
                name: "merchant_categories");

            migrationBuilder.DropTable(
                name: "order_contacts");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "order_promotion_items");

            migrationBuilder.DropTable(
                name: "order_promotion_sub_items");

            migrationBuilder.DropTable(
                name: "order_status_trackings");

            migrationBuilder.DropTable(
                name: "payment_method_trackings");

            migrationBuilder.DropTable(
                name: "product_product_attributes");

            migrationBuilder.DropTable(
                name: "product_promotion_items");

            migrationBuilder.DropTable(
                name: "product_variant_attribute_values");

            migrationBuilder.DropTable(
                name: "shop_collection_product");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "voucher_product");

            migrationBuilder.DropTable(
                name: "discounts");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "order_promotions");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "product_promotions");

            migrationBuilder.DropTable(
                name: "attribute_values");

            migrationBuilder.DropTable(
                name: "product_variants");

            migrationBuilder.DropTable(
                name: "shop_collections");

            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "product_attributes");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "merchants");
        }
    }
}
