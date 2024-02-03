namespace OrdiniService.Models.ExternalAPI.Entity
{
    public class ProductAvailableForRequestApiDTO
    {
        public long IdProduct { get; set; }
        public int AvailableAmount { get; set; }

        public ProductAvailableForRequestApiDTO(long idProduct, int availableAmount)
        {
            this.IdProduct = idProduct;
            this.AvailableAmount = availableAmount;
        }

        public static ProductAvailableForRequestApiDTO ProductAvailableForRequestApiDTOFactory(long idProduct, int availableAmount)
        {
            return new ProductAvailableForRequestApiDTO(idProduct, availableAmount);
        }
    }
}
