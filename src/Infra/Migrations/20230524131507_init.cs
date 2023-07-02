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
                name: "BtcAsks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InsertAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Microtimestamp = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,8)", precision: 18, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BtcAsks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BtcBids",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InsertAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Microtimestamp = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,8)", precision: 18, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BtcBids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EthAsks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InsertAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Microtimestamp = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,8)", precision: 18, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EthAsks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EthBids",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InsertAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Microtimestamp = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,8)", precision: 18, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EthBids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    MinPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MediaAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    MediaPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MediaPrice5 = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    InsertAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountTotal = table.Column<decimal>(type: "numeric(18,8)", precision: 18, scale: 8, nullable: false),
                    InsertAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,8)", precision: 18, scale: 8, nullable: false),
                    Crypto = table.Column<string>(type: "text", nullable: false),
                    Stock = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BtcAsks_Amount",
                table: "BtcAsks",
                column: "Amount");

            migrationBuilder.CreateIndex(
                name: "IX_BtcAsks_Amount_Price",
                table: "BtcAsks",
                columns: new[] { "Amount", "Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BtcAsks_InsertAt",
                table: "BtcAsks",
                column: "InsertAt");

            migrationBuilder.CreateIndex(
                name: "IX_BtcAsks_Microtimestamp",
                table: "BtcAsks",
                column: "Microtimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_BtcAsks_Price",
                table: "BtcAsks",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_BtcBids_Amount",
                table: "BtcBids",
                column: "Amount");

            migrationBuilder.CreateIndex(
                name: "IX_BtcBids_Amount_Price",
                table: "BtcBids",
                columns: new[] { "Amount", "Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BtcBids_InsertAt",
                table: "BtcBids",
                column: "InsertAt");

            migrationBuilder.CreateIndex(
                name: "IX_BtcBids_Microtimestamp",
                table: "BtcBids",
                column: "Microtimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_BtcBids_Price",
                table: "BtcBids",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_EthAsks_Amount",
                table: "EthAsks",
                column: "Amount");

            migrationBuilder.CreateIndex(
                name: "IX_EthAsks_Amount_Price",
                table: "EthAsks",
                columns: new[] { "Amount", "Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EthAsks_InsertAt",
                table: "EthAsks",
                column: "InsertAt");

            migrationBuilder.CreateIndex(
                name: "IX_EthAsks_Microtimestamp",
                table: "EthAsks",
                column: "Microtimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_EthAsks_Price",
                table: "EthAsks",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_EthBids_Amount",
                table: "EthBids",
                column: "Amount");

            migrationBuilder.CreateIndex(
                name: "IX_EthBids_Amount_Price",
                table: "EthBids",
                columns: new[] { "Amount", "Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EthBids_InsertAt",
                table: "EthBids",
                column: "InsertAt");

            migrationBuilder.CreateIndex(
                name: "IX_EthBids_Microtimestamp",
                table: "EthBids",
                column: "Microtimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_EthBids_Price",
                table: "EthBids",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InsertAt",
                table: "Orders",
                column: "InsertAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BtcAsks");

            migrationBuilder.DropTable(
                name: "BtcBids");

            migrationBuilder.DropTable(
                name: "EthAsks");

            migrationBuilder.DropTable(
                name: "EthBids");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
