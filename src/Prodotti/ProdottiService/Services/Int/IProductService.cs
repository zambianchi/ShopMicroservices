using ProdottiService.Models.API;

namespace ProdottiService.Services.Int
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts(CancellationToken cancellationToken);
        Task<ProductDTO> GetProduct(long idProduct, CancellationToken cancellationToken);
    }
}
