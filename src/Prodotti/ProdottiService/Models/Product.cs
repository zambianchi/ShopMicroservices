namespace ProdottiService.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public int QuantitaDisponibile { get; set; }
        public long CategoryId { get; set; }
    }
}
