using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace product.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSaleInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "product_sale_info_id",
                table: "products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "product_sale_infos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    price = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_sale_infos", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_sale_infos_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_sale_infos_product_id",
                table: "product_sale_infos",
                column: "product_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_sale_infos");

            migrationBuilder.DropColumn(
                name: "product_sale_info_id",
                table: "products");
        }
    }
}
