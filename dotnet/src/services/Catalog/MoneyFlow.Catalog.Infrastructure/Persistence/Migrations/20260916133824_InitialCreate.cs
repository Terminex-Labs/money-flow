using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyFlow.Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: @"CREATE COLLATION case_insensitive (
                    PROVIDER = 'icu',
                    LOCALE = 'und-u-ks-level2',
                    DETERMINISTIC = FALSE);",
                suppressTransaction: true);

            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    unicode = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    full_name = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_accounts", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "type_accounts");
        }
    }
}
