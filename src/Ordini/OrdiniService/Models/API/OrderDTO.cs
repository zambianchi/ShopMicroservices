namespace OrdiniService.Models.API
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long CreationAccountId { get; set; }
        public List<long> ProductIds { get; set; }

        public OrderDTO(long id, DateTime date, long creationAccountId, List<long> productIds)
        {
            this.Id = id;
            this.Date = date;
            this.CreationAccountId = creationAccountId;
            this.ProductIds = productIds;
        }

        public static OrderDTO OrderDTOFactory(long id, DateTime date, long creationAccountId, List<long> productIds)
        {
            return new OrderDTO(id, date, creationAccountId, productIds);
        }
    }
}
