using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;
using ApiGateway.Models.ExternalAPI.Entity;
using ApiGateway.SubService.Int;
using ShopCommons.Helper;
using ShopCommons.Models;

namespace ApiGateway.SubService
{
    public partial class ApiGatewaySubService : IApiGatewayProdottiSubService
    {
        /// <summary>
        /// Carica prodotti specifici
        /// </summary>
        public async Task<List<ProductDTO>> GetSpecificProducts(List<long> idsProduct, CancellationToken cancellationToken)
        {
            var requestProducts = RabbitMQMessageRequest<List<long>>.RabbitMQMessageRequest_PRODUCTS_LIST_Factory(idsProduct);

            // Prendo i prodotti
            var getProductsResponse = await RabbitServiceMessageWrapper.SendRPC<List<long>, List<ProductApiDTO>>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_ProductRabbitMQQueue, requestProducts, cancellationToken);

            List<ProductDTO> products = new List<ProductDTO>();
            foreach (var product in getProductsResponse)
            {
                var orderProduct = ProductDTO.ProductDTOFactory(product.Id, product.Nome, product.Descrizione, product.Prezzo, product.QuantitaDisponibile);
                products.Add(orderProduct);
            }

            return products;
        }

        /// <summary>
        /// Carica un prodotto
        /// </summary>
        public async Task<ProductDTO> GetProduct(long idProduct, CancellationToken cancellationToken)
        {
            var getProductRequest = RabbitMQMessageRequest<long>.RabbitMQMessageRequest_GET_PRODUCT_Factory(idProduct);

            // Prendo il prodotto
            var getProductsResponse = await RabbitServiceMessageWrapper.SendRPC<long, ProductApiDTO>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_ProductRabbitMQQueue, getProductRequest, cancellationToken);

            return ProductDTO.ProductDTOFactory(getProductsResponse.Id, getProductsResponse.Nome, getProductsResponse.Descrizione, getProductsResponse.Prezzo, getProductsResponse.QuantitaDisponibile);
        }

        /// <summary>
        /// Crea un prodotto
        /// </summary>
        public async Task<ProductDTO> CreateProduct(CreateProductRequestDTO request, CancellationToken cancellationToken)
        {
            var createProductRequest = RabbitMQMessageRequest<CreateProductRequestDTO>.RabbitMQMessageRequest_CREATE_PRODUCT_Factory(request);

            // Creo il prodotto
            var createProductResponse = await RabbitServiceMessageWrapper.SendRPC<CreateProductRequestDTO, ProductApiDTO>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_ProductRabbitMQQueue, createProductRequest, cancellationToken);

            return ProductDTO.ProductDTOFactory(createProductResponse.Id, createProductResponse.Nome, createProductResponse.Descrizione, createProductResponse.Prezzo, createProductResponse.QuantitaDisponibile);
        }

        /// <summary>
        /// Elimina un prodotto
        /// </summary>
        public async Task DeleteProduct(long idProduct, CancellationToken cancellationToken)
        {
            var deleteProductRequest = RabbitMQMessageRequest<long>.RabbitMQMessageRequest_DELETE_PRODUCT_Factory(idProduct);

            // Elimino il prodotto
            await RabbitServiceMessageWrapper.SendRPC<long, bool>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_ProductRabbitMQQueue, deleteProductRequest, cancellationToken);
        }

        /// <summary>
        /// Modifica un prodotto
        /// </summary>
        public async Task<ProductDTO> EditProduct(EditProductRequestDTO request, CancellationToken cancellationToken)
        {
            var editProductRequest = RabbitMQMessageRequest<EditProductRequestDTO>.RabbitMQMessageRequest_EDIT_PRODUCT_Factory(request);

            // Modifica il prodotto
            var editProductResponse = await RabbitServiceMessageWrapper.SendRPC<EditProductRequestDTO, ProductApiDTO>(this._rabbitServiceHelper, this._environmentSettings.EnvironmentName_ProductRabbitMQQueue, editProductRequest, cancellationToken);

            return ProductDTO.ProductDTOFactory(editProductResponse.Id, editProductResponse.Nome, editProductResponse.Descrizione, editProductResponse.Prezzo, editProductResponse.QuantitaDisponibile);
        }
    }
}
