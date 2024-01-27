using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdiniService.Migrations
{
    /// <inheritdoc />
    public partial class OrdiniModelInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationAccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { nameof(Models.Order.Id), nameof(Models.Order.Date), nameof(Models.Order.CreationAccountId) },
                values: new object[,] {
                    { 1, new DateTime(2024, 01, 26, 17, 23, 23), 1 },
                    { 2, new DateTime(2024, 01, 27, 03, 02, 54), 1 },
                    { 3, new DateTime(2024, 01, 27, 04, 11, 03), 1 },
                    { 4, new DateTime(2024, 01, 27, 08, 00, 00), 2 },
                    { 5, new DateTime(2024, 01, 27, 10, 00, 00), 2 },
                    { 6, new DateTime(2024, 01, 27, 12, 00, 00), 2 },
                    { 7, new DateTime(2024, 01, 27, 14, 00, 00), 3 },
                    { 8, new DateTime(2024, 01, 27, 16, 00, 00), 3 },
                    { 9, new DateTime(2024, 01, 27, 18, 00, 00), 3 },
                    { 10, new DateTime(2024, 01, 27, 20, 00, 00), 4 },
                    { 11, new DateTime(2024, 01, 27, 22, 00, 00), 4 },
                    { 12, new DateTime(2024, 01, 28, 00, 00, 00), 4 },
                    { 13, new DateTime(2024, 01, 28, 02, 00, 00), 5 },
                    { 14, new DateTime(2024, 01, 28, 04, 00, 00), 5 },
                    { 15, new DateTime(2024, 01, 28, 06, 00, 00), 5 },
                    { 16, new DateTime(2024, 01, 28, 08, 00, 00), 6 },
                    { 17, new DateTime(2024, 01, 28, 10, 00, 00), 6 },
                    { 18, new DateTime(2024, 01, 28, 12, 00, 00), 6 },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
