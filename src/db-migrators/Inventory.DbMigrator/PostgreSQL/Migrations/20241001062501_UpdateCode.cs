using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_products_code",
                table: "products",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_product_variants_code",
                table: "product_variants",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_products_code",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_product_variants_code",
                table: "product_variants");
        }
    }
}
