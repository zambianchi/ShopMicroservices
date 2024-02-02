namespace ApiGateway.Models.API.Entity
{
    public class ProductDTO
    {
        public long IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }

        public ProductDTO(long idProduct, string name, string description, double price, int amount)
        {
            this.IdProduct = idProduct;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Amount = amount;
        }

        public static ProductDTO ProductDTOFactory(long idProduct, string name, string description, double price, int amount)
        {
            return new ProductDTO(idProduct, name, description, price, amount);
        }
    }
}
