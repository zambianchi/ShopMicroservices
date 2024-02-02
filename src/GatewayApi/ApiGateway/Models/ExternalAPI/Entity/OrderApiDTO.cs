namespace ApiGateway.Models.ExternalAPI.Entity
{
    public class OrderApiDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long CreationAccountId { get; set; }
        public List<long> ProductIds { get; set; }
    }
}
