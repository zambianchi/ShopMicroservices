namespace ProdottiService.Models.API.Request
{
    public class EditProductAvailableAmountRequest
    {
        public long IdProduct { get; set; }
        public int AvailableAmount { get; set; }

        public EditProductAvailableAmountRequest(long idProduct, int availableAmount)
        {
            this.IdProduct = idProduct;
            this.AvailableAmount = availableAmount;
        }

        public static EditProductAvailableAmountRequest EditProductAvailableAmountRequestFactory(long idProduct, int availableAmount)
        {
            return new EditProductAvailableAmountRequest(idProduct, availableAmount);
        }
    }
}
