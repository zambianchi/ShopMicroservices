namespace OrdiniService.Models.API.Entity
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long CreationAccountId { get; set; }
        public List<OrderProductsDTO> Products { get; set; }

        public OrderDTO(long id, DateTime date, long creationAccountId, List<OrderProductsDTO> products)
        {
            this.Id = id;
            this.Date = date;
            this.CreationAccountId = creationAccountId;
            this.Products = products;
        }

        public static OrderDTO OrderDTOFactory(long id, DateTime date, long creationAccountId, List<OrderProductsDTO> products)
        {
            return new OrderDTO(id, date, creationAccountId, products);
        }
    }
}
