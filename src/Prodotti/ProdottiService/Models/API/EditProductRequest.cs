namespace ProdottiService.Models.API
{
    public class EditProductRequest
    {
        public long IdProdotto { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public int QuantitaDisponibile { get; set; }

        public EditProductRequest(long idProdotto, string nome, string descrizione, double prezzo, int quantitaDisponibile)
        {
            this.IdProdotto = idProdotto;
            this.Nome = nome;
            this.Descrizione = descrizione;
            this.Prezzo = prezzo;
            this.QuantitaDisponibile = quantitaDisponibile;
        }

        public static EditProductRequest EditProductRequestFactory(long idProdotto, string nome, string descrizione, double prezzo, int quantitaDisponibile)
        {
            return new EditProductRequest(idProdotto, nome, descrizione, prezzo, quantitaDisponibile);
        }
    }
}
