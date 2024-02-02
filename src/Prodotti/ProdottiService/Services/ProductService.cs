using Microsoft.EntityFrameworkCore;
using ProdottiService.Context;
using ProdottiService.Models;
using ProdottiService.Models.API.Entity;
using ProdottiService.Models.API.Request;
using ProdottiService.Models.DB;
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

        /// <summary>
        /// Carica tutti i prodotti
        /// </summary>
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

        /// <summary>
        /// Carica un prodotto
        /// </summary>
        public async Task<ProductDTO> GetProduct(long idProduct, CancellationToken cancellationToken)
        {
            var productDB = await _productsContext.Products
                .Where(x => x.Id == idProduct)
                .SingleAsync(cancellationToken);

            return ProductDTO.ProductDTOFactory(productDB.Id, productDB.Nome, productDB.Descrizione, productDB.Prezzo, productDB.QuantitaDisponibile);
        }

        /// <summary>
        /// Carica specifici prodotti
        /// </summary>
        public async Task<List<ProductDTO>> GetSpecificProducts(List<long> idsProduct, CancellationToken cancellationToken)
        {
            List<ProductDTO> products = [];

            var productsDB = await _productsContext.Products
                .Where(x => idsProduct.Contains(x.Id))
                .ToListAsync(cancellationToken);

            foreach (var productDB in productsDB)
            {
                products.Add(ProductDTO.ProductDTOFactory(productDB.Id, productDB.Nome, productDB.Descrizione, productDB.Prezzo, productDB.QuantitaDisponibile));
            }

            return products;
        }

        /// <summary>
        /// Crea un prodotto
        /// </summary>
        public async Task<ProductDTO> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
        {
            // Controllo possibile presenza di un prodotto con lo stesso nome
            var sameNameProductDB = await _productsContext.Products
                .Where(x => x.Nome == request.Name)
                .AnyAsync(cancellationToken);

            // Se esiste già un prodotto con lo stesso nome
            if (sameNameProductDB)
            {
                throw new Exception("Prodotto già esistente");
            }

            var productDB = new Product
            {
                Nome = request.Name,
                Descrizione = request.Description,
                Prezzo = request.Price,
                QuantitaDisponibile = request.AvailableAmount
            };

            await _productsContext.Products
                .AddAsync(productDB, cancellationToken);

            await _productsContext.SaveChangesAsync(cancellationToken);

            return ProductDTO.ProductDTOFactory(productDB.Id, productDB.Nome, productDB.Descrizione, productDB.Prezzo, productDB.QuantitaDisponibile);
        }

        /// <summary>
        /// Cancella un prodotto
        /// </summary>
        public async Task DeleteProduct(long idProduct, CancellationToken cancellationToken)
        {
            var productDB = await _productsContext.Products
                .Where(x => x.Id == idProduct)
                .SingleAsync(cancellationToken);

            _productsContext.Products.Remove(productDB);

            await _productsContext.SaveChangesAsync(cancellationToken);

            // Possibile invio fanout RabbitMQ per eliminazione articolo
        }

        /// <summary>
        /// Modifica prodotto
        /// </summary>
        public async Task<ProductDTO> EditProduct(EditProductRequest request, CancellationToken cancellationToken)
        {
            var productDB = await _productsContext.Products
                .Where(x => x.Id == request.IdProduct)
                .SingleAsync(cancellationToken);

            productDB.Nome = request.Name;
            productDB.Descrizione = request.Description;
            productDB.Prezzo = request.Price;
            productDB.CategoryId = request.CategoryId;
            productDB.QuantitaDisponibile = request.AvailableAmount;

            await _productsContext.SaveChangesAsync(cancellationToken);

            return ProductDTO.ProductDTOFactory(productDB.Id, productDB.Nome, productDB.Descrizione, productDB.Prezzo, productDB.QuantitaDisponibile);
        }

        /// <summary>
        /// Modifica quantità disponibile prodotto
        /// </summary>
        public async Task<ProductDTO> EditProductAvailableAmount(EditProductAvailableAmountRequest request, CancellationToken cancellationToken)
        {
            var productDB = await _productsContext.Products
                .Where(x => x.Id == request.IdProduct)
                .SingleAsync(cancellationToken);

            productDB.QuantitaDisponibile -= request.AvailableAmount;

            if (productDB.QuantitaDisponibile < 0)
            {
                throw new InvalidOperationException("Quantità inferiore a 0");
            }

            await _productsContext.SaveChangesAsync(cancellationToken);

            return ProductDTO.ProductDTOFactory(productDB.Id, productDB.Nome, productDB.Descrizione, productDB.Prezzo, productDB.QuantitaDisponibile);
        }

        /// <summary>
        /// Modifica quantità disponibile prodotti
        /// </summary>
        public async Task EditProductsAvailableAmount(EditProductsAvailableAmountRequest request, CancellationToken cancellationToken)
        {
            foreach (var product in request.Products)
            {
                var productDB = await _productsContext.Products
                    .Where(x => x.Id == product.IdProduct)
                    .SingleAsync(cancellationToken);

                productDB.QuantitaDisponibile -= product.AvailableAmount;

                if (productDB.QuantitaDisponibile < 0)
                {
                    throw new InvalidOperationException("Quantità inferiore a 0");
                }

                await _productsContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
