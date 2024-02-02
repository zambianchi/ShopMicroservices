namespace ProdottiService.Models.API.Request
{
    public class EditProductRequest
    {
        public long IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AvailableAmount { get; set; }
        public long CategoryId { get; set; }

        public EditProductRequest(long idProduct, string name, string description, double price, int availableAmount, long categoryId)
        {
            this.IdProduct = idProduct;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.AvailableAmount = availableAmount;
            this.CategoryId = categoryId;
        }

        public static EditProductRequest EditProductRequestFactory(long idProduct, string name, string description, double price, int availableAmount, long categoryId)
        {
            return new EditProductRequest(idProduct, name, description, price, availableAmount, categoryId);
        }
    }
}
