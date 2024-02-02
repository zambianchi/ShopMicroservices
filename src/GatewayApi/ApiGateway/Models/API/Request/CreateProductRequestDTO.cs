namespace ApiGateway.Models.API.Request
{
    public class CreateProductRequestDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AvailableAmount { get; set; }
    }
}
