namespace OrdiniService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public long CreationAccountId { get; set; }
        public long DeliveryAddressId { get; set; }
    }
}
