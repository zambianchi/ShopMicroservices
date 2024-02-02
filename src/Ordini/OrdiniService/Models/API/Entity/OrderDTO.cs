namespace OrdiniService.Models.API.Entity
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long CreationAccountId { get; set; }
        public List<long> ProductIds { get; set; }

        public OrderDTO(long id, DateTime date, long creationAccountId, List<long> productIds)
        {
            Id = id;
            Date = date;
            CreationAccountId = creationAccountId;
            ProductIds = productIds;
        }

        public static OrderDTO OrderDTOFactory(long id, DateTime date, long creationAccountId, List<long> productIds)
        {
            return new OrderDTO(id, date, creationAccountId, productIds);
        }
    }
}
