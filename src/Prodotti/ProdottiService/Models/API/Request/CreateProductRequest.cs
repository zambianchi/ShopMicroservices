namespace ProdottiService.Models.API.Request
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AvailableAmount { get; set; }

        public CreateProductRequest(string name, string description, double price, int availableAmount)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.AvailableAmount = availableAmount;
        }

        public static CreateProductRequest CreateProductRequestFactory(string name, string description, double price, int availableAmount)
        {
            return new CreateProductRequest(name, description, price, availableAmount);
        }
    }
}
