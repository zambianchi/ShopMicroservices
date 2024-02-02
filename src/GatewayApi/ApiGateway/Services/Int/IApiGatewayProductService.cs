using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;

namespace ApiGateway.Services.Int
{
    public interface IApiGatewayProductService
    {
        Task<ProductDTO> GetProduct(long idProduct, CancellationToken cancellationToken);
        Task<ProductDTO> CreateProduct(CreateProductRequestDTO request, CancellationToken cancellationToken);
        Task DeleteProduct(long idOrder, CancellationToken cancellationToken);
        Task<ProductDTO> EditProduct(EditProductRequestDTO request, CancellationToken cancellationToken);
    }
}
