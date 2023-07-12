using System;
using System.Collections.Generic;
using ECommerce.Services.Orders;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace order.Migrations
{
    /// <inheritdoc />
    public partial class InitOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:unaccent", ",,");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    total_price = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    total_quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    order_items = table.Column<List<OrderItem>>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
