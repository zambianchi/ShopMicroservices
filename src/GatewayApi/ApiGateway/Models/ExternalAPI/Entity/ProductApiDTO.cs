namespace ApiGateway.Models.ExternalAPI.Entity
{
    public class ProductApiDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public int QuantitaDisponibile { get; set; }
    }
}
