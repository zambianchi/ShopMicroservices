namespace ProdottiService.Models.API.Request
{
    public class ProductAvailableForRequest
    {
        public long IdProduct { get; set; }
        public int AvailableAmount { get; set; }

        public ProductAvailableForRequest(long idProduct, int availableAmount)
        {
            this.IdProduct = idProduct;
            this.AvailableAmount = availableAmount;
        }

        public static ProductAvailableForRequest ProductAvailableForRequestFactory(long idProduct, int availableAmount)
        {
            return new ProductAvailableForRequest(idProduct, availableAmount);
        }
    }
}
