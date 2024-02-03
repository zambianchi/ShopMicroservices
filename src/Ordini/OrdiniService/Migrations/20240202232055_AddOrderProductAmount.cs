using Microsoft.EntityFrameworkCore.Migrations;
using OrdiniService.Models.DB;

#nullable disable

namespace OrdiniService.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderProductAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);


            // Mock data OrderProducts
            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { nameof(OrderProducts.IdProduct), "OrderId", nameof(OrderProducts.Amount) },
                values: new object[,] {
                                { 1, 1, 5 },
                                { 2, 1, 5 },
                                { 4, 1, 5 },
                                { 5, 1, 5 },
                                { 6, 2, 5 },
                                { 4, 3, 5 },
                                { 5, 3, 5 },
                                { 9, 4, 5 },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "OrderProducts");
        }
    }
}
