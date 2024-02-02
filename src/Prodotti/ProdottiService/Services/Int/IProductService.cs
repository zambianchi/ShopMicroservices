using ProdottiService.Models.API.Entity;
using ProdottiService.Models.API.Request;

namespace ProdottiService.Services.Int
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts(CancellationToken cancellationToken);
        Task<ProductDTO> GetProduct(long idProduct, CancellationToken cancellationToken);
        Task<List<ProductDTO>> GetSpecificProducts(List<long> idsProduct, CancellationToken cancellationToken);
        Task<ProductDTO> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken);
        Task DeleteProduct(long idProduct, CancellationToken cancellationToken);
        Task<ProductDTO> EditProduct(EditProductRequest request, CancellationToken cancellationToken);
    }
}
