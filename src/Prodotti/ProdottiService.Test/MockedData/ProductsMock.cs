﻿namespace ProdottiService.Test.MockedData
{
    public class ProductsMock
    {
        public static List<Models.Product> GetMockedProducts()
        {
            return new List<Models.Product>
            {
                new Models.Product{ Id = 1, Nome = "Stampa fotografica", Descrizione = "Qualità standard", Prezzo = 0.3, QuantitaDisponibile = 140 },
                new Models.Product{ Id = 2, Nome = "Stampa alta risoluzione", Descrizione = "Qualità HD", Prezzo = 0.3, QuantitaDisponibile = 85 },
                new Models.Product{ Id = 3, Nome = "Cartolina", Descrizione = "Cartolina 15x30", Prezzo = 0.7, QuantitaDisponibile = 170 },
                new Models.Product{ Id = 4, Nome = "Fotoritocco", Descrizione = "Piccola immagine 10x10", Prezzo = 0.2, QuantitaDisponibile = 20 },
            };
        }
    }
}
