using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdiniService.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationAccountId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryAddressId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    IdProduct = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.IdProduct, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");


            // Mock data Orders
            migrationBuilder.InsertData(
                table: "Orders",
                        columns: new[] { nameof(Models.Order.Id), nameof(Models.Order.Date), nameof(Models.Order.CreationAccountId), nameof(Models.Order.DeliveryAddressId) },
                        values: new object[,] {
                                { 1, new DateTime(2024, 01, 26, 17, 23, 23), 1, 1 },
                                { 2, new DateTime(2024, 01, 27, 03, 02, 54), 1, 1 },
                                { 3, new DateTime(2024, 01, 27, 04, 11, 03), 1, 1 },
                                { 4, new DateTime(2024, 01, 27, 08, 00, 00), 2, 1 },
                        });

            // Mock data OrderProducts
            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { nameof(Models.OrderProducts.IdProduct), "OrderId" },
                values: new object[,] {
                                { 1, 1 },
                                { 2, 1 },
                                { 4, 1 },
                                { 5, 1 },
                                { 6, 2 },
                                { 4, 3 },
                                { 5, 3 },
                                { 9, 4 },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
