using Microsoft.EntityFrameworkCore;
using ProdottiService.Context;
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

        public async Task<ProductDTO> GetProduct(long idOrder, CancellationToken cancellationToken)
        {
            var productDB = await _productsContext.Products
                .Where(x => x.Id == idOrder)
                .SingleAsync(cancellationToken);

            return ProductDTO.ProductDTOFactory(productDB.Id, productDB.Nome, productDB.Descrizione, productDB.Prezzo, productDB.QuantitaDisponibile);
        }

        public Task<ProductDTO> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
