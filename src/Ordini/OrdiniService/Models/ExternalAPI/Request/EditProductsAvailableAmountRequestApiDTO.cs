using OrdiniService.Models.ExternalAPI.Entity;

namespace OrdiniService.Models.ExternalAPI.Request
{
    public class EditProductsAvailableAmountRequestApiDTO
    {
        public List<ProductAvailableForRequestApiDTO> Products { get; set; }

        public EditProductsAvailableAmountRequestApiDTO(List<ProductAvailableForRequestApiDTO> products)
        {
            this.Products = products;
        }

        public static EditProductsAvailableAmountRequestApiDTO EditProductsAvailableAmountRequestApiDTOFactory(List<ProductAvailableForRequestApiDTO> products)
        {
            return new EditProductsAvailableAmountRequestApiDTO(products);
        }
    }
}
