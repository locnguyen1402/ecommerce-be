using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Inventory.DbMigrator.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShopCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_shop_collections_shop_collections_parent_id",
                table: "shop_collections");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "shop_collections",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValueSql: "''");

            migrationBuilder.AddForeignKey(
                name: "fk_shop_collections_shop_collections_parent_id",
                table: "shop_collections",
                column: "parent_id",
                principalTable: "shop_collections",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_shop_collections_shop_collections_parent_id",
                table: "shop_collections");

            migrationBuilder.DropColumn(
                name: "description",
                table: "shop_collections");

            migrationBuilder.AddForeignKey(
                name: "fk_shop_collections_shop_collections_parent_id",
                table: "shop_collections",
                column: "parent_id",
                principalTable: "shop_collections",
                principalColumn: "id");
        }
    }
}
