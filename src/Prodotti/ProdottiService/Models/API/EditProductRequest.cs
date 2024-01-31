namespace ProdottiService.Models.API
{
    public class EditProductRequest
    {
        public long IdProdotto { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public int QuantitaDisponibile { get; set; }
        public long CategoryId { get; set; }

        public EditProductRequest(long idProdotto, string nome, string descrizione, double prezzo, int quantitaDisponibile, long categoryId)
        {
            this.IdProdotto = idProdotto;
            this.Nome = nome;
            this.Descrizione = descrizione;
            this.Prezzo = prezzo;
            this.QuantitaDisponibile = quantitaDisponibile;
            this.CategoryId = categoryId;
        }

        public static EditProductRequest EditProductRequestFactory(long idProdotto, string nome, string descrizione, double prezzo, int quantitaDisponibile, long categoryId)
        {
            return new EditProductRequest(idProdotto, nome, descrizione, prezzo, quantitaDisponibile, categoryId);
        }
    }
}
