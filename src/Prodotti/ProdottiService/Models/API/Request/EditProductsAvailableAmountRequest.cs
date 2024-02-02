namespace ProdottiService.Models.API.Request
{
    public class EditProductsAvailableAmountRequest
    {
        public List<ProductAvailableForRequest> Products { get; set; }

        public EditProductsAvailableAmountRequest(List<ProductAvailableForRequest> products)
        {
            this.Products = products;
        }

        public static EditProductsAvailableAmountRequest EditProductsAvailableAmountRequestFactory(List<ProductAvailableForRequest> products)
        {
            return new EditProductsAvailableAmountRequest(products);
        }
    }
}
