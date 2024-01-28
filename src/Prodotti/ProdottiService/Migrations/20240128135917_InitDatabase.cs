using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProdottiService.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo = table.Column<double>(type: "float", nullable: false),
                    QuantitaDisponibile = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { nameof(Models.Product.Id), nameof(Models.Product.Nome), nameof(Models.Product.Descrizione), nameof(Models.Product.Prezzo),
                                 nameof(Models.Product.QuantitaDisponibile), nameof(Models.Product.CategoryId) },
                values: new object[,] {
                    { 1, "Stampa fotografica", "Qualità standard", 0.3, 140, 1 },
                    { 2, "Stampa alta risoluzione", "Qualità HD", 0.5, 85, 1 },
                    { 3, "Cartolina", "Cartolina 15x30", 0.7, 170, 1 },
                    { 4, "Fotoritocco", "Piccola immagine 10x10", 0.2, 20, 1 },
                    { 5, "Foto artistica", "Stampa su tela", 0.6, 110, 1 },
                    { 6, "Poster personalizzato", "Dimensioni 50x70", 6.8, 75, 1 },
                    { 7, "Fotolibro", "20 pagine personalizzate", 15.2, 50, 1 },
                    { 8, "Stampe panoramiche", "Formato 30x60", 3.9, 65, 1 },
                    { 9, "Foto puzzle", "1000 pezzi", 3.5, 30, 1 },
                    { 10, "Calendario fotografico", "Anno personalizzato", 10.0, 45, 1 },
                    { 11, "Foto su tazza", "Tazza standard", 0.4, 90, 1 },
                    { 12, "Foto su cuscino", "Cuscino 40x40", 1.3, 40, 1 },
                    { 13, "Pannello fotografico", "Dimensioni 60x80", 1.8, 25, 1 },
                    { 14, "Mini foto magnetiche", "Set da 10", 3.6, 55, 1 },
                    { 15, "Foto su cover per cellulare", "Modello universale", 0.7, 60, 1 },
                    { 16, "Foto su maglietta", "Taglia M", 4.0, 35, 1 },
                    { 17, "Foto su puzzle 3D", "500 pezzi", 5.4, 30, 1 },
                    { 18, "Foto su mousepad", "Dimensioni standard", 3.5, 70, 1 },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
