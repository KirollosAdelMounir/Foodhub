using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodHub.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order_FoodItem");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Order",
                newName: "OrderDate");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "fooditemId",
                table: "Order",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Order_fooditemId",
                table: "Order",
                column: "fooditemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_FoodItem_fooditemId",
                table: "Order",
                column: "fooditemId",
                principalTable: "FoodItem",
                principalColumn: "FoodItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_FoodItem_fooditemId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_fooditemId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "fooditemId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Order",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<float>(
                name: "TotalPrice",
                table: "Order",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Order_FoodItem",
                columns: table => new
                {
                    FoodItemId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_FoodItem", x => new { x.FoodItemId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_Order_FoodItem_FoodItem_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItem",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_FoodItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_FoodItem_OrderId",
                table: "Order_FoodItem",
                column: "OrderId");
        }
    }
}
