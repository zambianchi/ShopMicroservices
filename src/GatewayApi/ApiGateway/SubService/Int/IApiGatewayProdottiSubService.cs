using ApiGateway.Models.API.Entity;
using ApiGateway.Models.API.Request;

namespace ApiGateway.SubService.Int
{
    public interface IApiGatewayProdottiSubService
    {
        Task<List<ProductDTO>> GetSpecificProducts(List<long> idsProduct, CancellationToken cancellationToken);
        Task<ProductDTO> GetProduct(long idProduct, CancellationToken cancellationToken);
        Task<ProductDTO> CreateProduct(CreateProductRequestDTO request, CancellationToken cancellationToken);
        Task DeleteProduct(long idProduct, CancellationToken cancellationToken);
        Task<ProductDTO> EditProduct(EditProductRequestDTO request, CancellationToken cancellationToken);
    }
}
