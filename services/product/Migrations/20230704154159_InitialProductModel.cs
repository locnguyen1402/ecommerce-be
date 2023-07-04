using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace product.Migrations
{
    /// <inheritdoc />
    public partial class InitialProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    author = table.Column<string>(type: "text", nullable: false),
                    publication_year = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    publisher = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValue: "''"),
                    image_url_s = table.Column<string>(type: "text", nullable: true),
                    image_url_m = table.Column<string>(type: "text", nullable: true),
                    image_url_l = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
