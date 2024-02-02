using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;
using ApiGateway.Services.Int;
using ApiGateway.SubService.Int;

namespace ApiGateway.Services
{
    public class ApiGatewayProductService : IApiGatewayProductService
    {
        private readonly IApiGatewayProdottiSubService _apiGatewaySubService;

        public ApiGatewayProductService(IApiGatewayProdottiSubService apiGatewaySubService)
        {
            _apiGatewaySubService = apiGatewaySubService;
        }

        public async Task<ProductDTO> GetProduct(long idProduct, CancellationToken cancellationToken)
        {
            var getProductResult = await _apiGatewaySubService.GetProduct(idProduct, cancellationToken);
            return getProductResult;
        }

        public async Task<ProductDTO> CreateProduct(CreateProductRequestDTO request, CancellationToken cancellationToken)
        {
            var createProductResult = await _apiGatewaySubService.CreateProduct(request, cancellationToken);
            return createProductResult;
        }

        public async Task DeleteProduct(long idOrder, CancellationToken cancellationToken)
        {
            await _apiGatewaySubService.DeleteProduct(idOrder, cancellationToken);
        }

        public async Task<ProductDTO> EditProduct(EditProductRequestDTO request, CancellationToken cancellationToken)
        {
            var editProductResult = await _apiGatewaySubService.EditProduct(request, cancellationToken);
            return editProductResult;
        }
    }
}
