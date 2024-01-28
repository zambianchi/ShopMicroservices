namespace ProdottiService.Models.API
{
    public class CreateProductRequest
    {
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public int QuantitaDisponibile { get; set; }

        public CreateProductRequest(string nome, string descrizione, double prezzo, int quantitaDisponibile)
        {
            this.Nome = nome;
            this.Descrizione = descrizione;
            this.Prezzo = prezzo;
            this.QuantitaDisponibile = quantitaDisponibile;
        }

        public static CreateProductRequest CreateProductRequestFactory(string nome, string descrizione, double prezzo, int quantitaDisponibile)
        {
            return new CreateProductRequest(nome, descrizione, prezzo, quantitaDisponibile);
        }
    }
}
