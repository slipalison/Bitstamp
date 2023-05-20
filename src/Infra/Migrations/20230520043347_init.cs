using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EthAsk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Microtimestamp = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EthAsk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EthBid",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InsertAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Microtimestamp = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EthBid", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EthAsk_Amount",
                table: "EthAsk",
                column: "Amount");

            migrationBuilder.CreateIndex(
                name: "IX_EthAsk_InsertAt",
                table: "EthAsk",
                column: "InsertAt");

            migrationBuilder.CreateIndex(
                name: "IX_EthAsk_Price",
                table: "EthAsk",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_EthBid_Amount",
                table: "EthBid",
                column: "Amount");

            migrationBuilder.CreateIndex(
                name: "IX_EthBid_InsertAt",
                table: "EthBid",
                column: "InsertAt");

            migrationBuilder.CreateIndex(
                name: "IX_EthBid_Price",
                table: "EthBid",
                column: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EthAsk");

            migrationBuilder.DropTable(
                name: "EthBid");
        }
    }
}
