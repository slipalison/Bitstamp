using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class adduniqueindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EthBids_Amount_Price",
                table: "EthBids",
                columns: new[] { "Amount", "Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EthAsks_Amount_Price",
                table: "EthAsks",
                columns: new[] { "Amount", "Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BtcBids_Amount_Price",
                table: "BtcBids",
                columns: new[] { "Amount", "Price" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BtcAsks_Amount_Price",
                table: "BtcAsks",
                columns: new[] { "Amount", "Price" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EthBids_Amount_Price",
                table: "EthBids");

            migrationBuilder.DropIndex(
                name: "IX_EthAsks_Amount_Price",
                table: "EthAsks");

            migrationBuilder.DropIndex(
                name: "IX_BtcBids_Amount_Price",
                table: "BtcBids");

            migrationBuilder.DropIndex(
                name: "IX_BtcAsks_Amount_Price",
                table: "BtcAsks");
        }
    }
}
