using System;
using System.Collections.Generic;
using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.AggregatesModel.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ECommerce.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:unaccent", ",,");

            migrationBuilder.CreateTable(
                name: "applications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    application_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    client_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    client_secret = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    client_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    concurrency_token = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    consent_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    display_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    display_names = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, defaultValueSql: "'{}'"),
                    json_web_key_set = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'{}'"),
                    permissions = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, defaultValueSql: "'[]'"),
                    post_logout_redirect_uris = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'[]'"),
                    properties = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'{}'"),
                    redirect_uris = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'[]'"),
                    requirements = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, defaultValueSql: "'[]'"),
                    settings = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'{}'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, defaultValueSql: "''"),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
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
                name: "client_roles",
                columns: table => new
                {
                    role_name = table.Column<string>(type: "text", nullable: false),
                    client_id = table.Column<string>(type: "text", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_roles", x => new { x.client_id, x.role_name });
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    first_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    full_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "'UNSPECIFIED'"),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ref_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    level_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'SILVER'"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "data_protection_keys",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    friendly_name = table.Column<string>(type: "text", nullable: true),
                    xml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_data_protection_keys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "discounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
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
                name: "import_histories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    document_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    document = table.Column<Document>(type: "jsonb", nullable: false, defaultValueSql: "'{}'"),
                    remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    logs = table.Column<List<LogDetail>>(type: "jsonb", nullable: false, defaultValueSql: "'[]'"),
                    events = table.Column<List<ImportEvent>>(type: "jsonb", nullable: false, defaultValueSql: "'[]'"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_import_histories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "merchants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    merchant_number = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_merchants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "object_storages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    original_name = table.Column<string>(type: "text", nullable: false),
                    bucket = table.Column<string>(type: "text", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    extension = table.Column<string>(type: "text", nullable: true),
                    content_type = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    key = table.Column<string>(type: "text", nullable: false),
                    is_uploaded = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_object_storages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission_groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_attributes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    predefined = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_attributes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    predefined = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    permissions = table.Column<List<string>>(type: "text[]", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "''"),
                    normalized_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "''"),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scopes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    concurrency_token = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    descriptions = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, defaultValueSql: "'{}'"),
                    display_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    display_names = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, defaultValueSql: "'{}'"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValueSql: "''"),
                    properties = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'{}'"),
                    resources = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, defaultValueSql: "'[]'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scopes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, defaultValueSql: "''"),
                    gender = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    birth_date = table.Column<DateOnly>(type: "DATE", nullable: true),
                    picture = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'NEW'"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    user_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    normalized_user_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    normalized_email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    security_stamp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    concurrency_stamp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "authorizations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    application_id = table.Column<Guid>(type: "uuid", nullable: true),
                    concurrency_token = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    properties = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'{}'"),
                    scopes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, defaultValueSql: "'[]'"),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authorizations", x => x.id);
                    table.ForeignKey(
                        name: "fk_authorizations_applications_application_id",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    contact_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    address_info = table.Column<AddressInfo>(type: "jsonb", nullable: false, defaultValueSql: "'{}'"),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    start_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'NEW'"),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    sku = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, defaultValueSql: "''"),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    store_number = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''"),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    store_address = table.Column<string>(type: "text", nullable: true),
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                name: "vouchers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    code = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
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
                    merchant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vouchers", x => x.id);
                    table.ForeignKey(
                        name: "fk_vouchers_merchants_merchant_id",
                        column: x => x.merchant_id,
                        principalTable: "merchants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_permissions_permission_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "permission_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attribute_values",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    product_attribute_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                name: "role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    claim_value = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_claims_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "security_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    agent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    culture = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    protocol = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    schema = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    origin = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    uri = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    method = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    correlation_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    application_id = table.Column<Guid>(type: "uuid", nullable: true),
                    application_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    start_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    end_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    status_code = table.Column<int>(type: "integer", nullable: true),
                    exception = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    old_values = table.Column<string>(type: "jsonb", nullable: true),
                    new_values = table.Column<string>(type: "jsonb", nullable: true),
                    remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'OTHER'"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_security_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_security_events_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    claim_value = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_claims_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    provider_key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "''"),
                    provider_display_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_user_logins_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    login_provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "''"),
                    value = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_user_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    application_id = table.Column<Guid>(type: "uuid", nullable: true),
                    authorization_id = table.Column<Guid>(type: "uuid", nullable: true),
                    concurrency_token = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expired_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    payload = table.Column<string>(type: "text", nullable: true, defaultValueSql: "''"),
                    properties = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'{}'"),
                    redeemed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reference_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''"),
                    subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, defaultValueSql: "''"),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, defaultValueSql: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_tokens_applications_application_id",
                        column: x => x.application_id,
                        principalTable: "applications",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tokens_authorizations_authorization_id",
                        column: x => x.authorization_id,
                        principalTable: "authorizations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order_contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    address_info = table.Column<AddressInfo>(type: "jsonb", nullable: false, defaultValueSql: "'{}'"),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    value = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_promotion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                name: "shop_collection_product",
                columns: table => new
                {
                    shop_collection_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                name: "voucher_product",
                columns: table => new
                {
                    voucher_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_promotion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'UNSPECIFIED'"),
                    discount_price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    discount_percentage = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false, defaultValue: 0m),
                    no_products_per_order_limit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'SPECIFIC'"),
                    max_items_per_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_variant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_attribute_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    attribute_value_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                name: "ix_applications_client_id",
                table: "applications",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_attribute_values_product_attribute_id",
                table: "attribute_values",
                column: "product_attribute_id");

            migrationBuilder.CreateIndex(
                name: "ix_authorizations_application_id_status_subject_type",
                table: "authorizations",
                columns: new[] { "application_id", "status", "subject", "type" });

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
                name: "ix_permissions_group_id",
                table: "permissions",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "ix_permissions_value",
                table: "permissions",
                column: "value",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "name" });

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
                name: "ix_role_claims_role_id",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_normalized_name",
                table: "roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_scopes_name",
                table: "scopes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_security_events_user_id",
                table: "security_events",
                column: "user_id");

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
                name: "ix_tokens_application_id_status_subject_type",
                table: "tokens",
                columns: new[] { "application_id", "status", "subject", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_tokens_authorization_id",
                table: "tokens",
                column: "authorization_id");

            migrationBuilder.CreateIndex(
                name: "ix_tokens_reference_id",
                table: "tokens",
                column: "reference_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_claims_user_id",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_logins_user_id",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_normalized_email",
                table: "users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "ix_users_normalized_user_name",
                table: "users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_phone_number",
                table: "users",
                column: "phone_number");

            migrationBuilder.CreateIndex(
                name: "ix_voucher_product_product_id",
                table: "voucher_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_vouchers_merchant_id",
                table: "vouchers",
                column: "merchant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client_roles");

            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "data_protection_keys");

            migrationBuilder.DropTable(
                name: "discount_applied_to_categories");

            migrationBuilder.DropTable(
                name: "discount_applied_to_products");

            migrationBuilder.DropTable(
                name: "import_histories");

            migrationBuilder.DropTable(
                name: "merchant_categories");

            migrationBuilder.DropTable(
                name: "object_storages");

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
                name: "permissions");

            migrationBuilder.DropTable(
                name: "product_product_attributes");

            migrationBuilder.DropTable(
                name: "product_promotion_items");

            migrationBuilder.DropTable(
                name: "product_variant_attribute_values");

            migrationBuilder.DropTable(
                name: "role_claims");

            migrationBuilder.DropTable(
                name: "scopes");

            migrationBuilder.DropTable(
                name: "security_events");

            migrationBuilder.DropTable(
                name: "shop_collection_product");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "tokens");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

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
                name: "permission_groups");

            migrationBuilder.DropTable(
                name: "product_promotions");

            migrationBuilder.DropTable(
                name: "attribute_values");

            migrationBuilder.DropTable(
                name: "product_variants");

            migrationBuilder.DropTable(
                name: "shop_collections");

            migrationBuilder.DropTable(
                name: "authorizations");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "product_attributes");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "applications");

            migrationBuilder.DropTable(
                name: "merchants");
        }
    }
}
