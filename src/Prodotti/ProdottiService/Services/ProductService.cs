using Microsoft.EntityFrameworkCore;
using ProdottiService.Context;
using ProdottiService.Models;
using ProdottiService.Models.API;
using ProdottiService.Services.Int;

namespace ProdottiService.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsContext _productsContext;

        public ProductService(ProductsContext productsContext)
        {
            this._productsContext = productsContext;
        }

        public async Task<List<ProductDTO>> GetProducts(CancellationToken cancellationToken)
        {
            List<ProductDTO> products = [];

            var productsDB = await _productsContext.Products
                .ToListAsync();

            foreach (var productDB in productsDB)
            {
                products.Add(ProductDTO.ProductDTOFactory(productDB.Id, productDB.Nome, productDB.Descrizione, productDB.Prezzo, productDB.QuantitaDisponibile));
            }

            return products;
        }

        public async Task<ProductDTO> GetProduct(long idProduct, CancellationToken cancellationToken)
        {
            var productDB = await _productsContext.Products
                .Where(x => x.Id == idProduct)
                .SingleAsync(cancellationToken);

            return ProductDTO.ProductDTOFactory(productDB.Id, productDB.Nome, productDB.Descrizione, productDB.Prezzo, productDB.QuantitaDisponibile);
        }

        public async Task<ProductDTO> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
        {
            // Controllo possibile presenza di un prodotto con lo stesso nome
            var sameNameProductDB = await _productsContext.Products
                .Where(x => x.Nome == request.Nome)
                .AnyAsync(cancellationToken);

            // Se esiste già un prodotto con lo stesso nome
            if (sameNameProductDB)
            {
                throw new Exception("Prodotto già esistente");
            }

            var productDB = new Product
            {
                Nome = request.Nome,
                Descrizione = request.Descrizione,
                Prezzo = request.Prezzo,
                QuantitaDisponibile = request.QuantitaDisponibile
            };

            await _productsContext.Products
                .AddAsync(productDB, cancellationToken);

            return ProductDTO.ProductDTOFactory(productDB.Id, productDB.Nome, productDB.Descrizione, productDB.Prezzo, productDB.QuantitaDisponibile);
        }
    }
}
