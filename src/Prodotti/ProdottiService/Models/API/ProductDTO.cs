namespace ProdottiService.Models.API
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public int QuantitaDisponibile { get; set; }

        public ProductDTO(long id, string nome, string descrizione, double prezzo, int quantitaDisponibile)
        {
            this.Id = id;
            this.Nome = nome;
            this.Descrizione = descrizione;
            this.Prezzo = prezzo;
            this.QuantitaDisponibile = quantitaDisponibile;
        }

        public static ProductDTO ProductDTOFactory(long id, string nome, string descrizione, double prezzo, int quantitaDisponibile)
        {
            return new ProductDTO(id, nome, descrizione, prezzo, quantitaDisponibile);
        }
    }
}
