namespace OrdiniService.Models.API
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long CreationAccountId { get; set; }

        public OrderDTO(long id, DateTime date, long creationAccountId)
        {
            this.Id = id;
            this.Date = date;
            this.CreationAccountId = creationAccountId;
        }

        public static OrderDTO OrderDTOFactory(long id, DateTime date, long creationAccountId)
        {
            return new OrderDTO(id, date, creationAccountId);
        }
    }
}
