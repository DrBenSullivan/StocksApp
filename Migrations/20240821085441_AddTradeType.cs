using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StocksApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTradeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TradeType",
                table: "SellOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TradeType",
                table: "BuyOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradeType",
                table: "SellOrders");

            migrationBuilder.DropColumn(
                name: "TradeType",
                table: "BuyOrders");
        }
    }
}
