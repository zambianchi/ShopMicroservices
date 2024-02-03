namespace ProdottiService.Models.API.Entity
{
    public class ProductAvailabilityDTO
    {
        public long Id { get; set; }
        public int Availability { get; set; }

        public ProductAvailabilityDTO(long id,int availability)
        {
            this.Id = id;
            this.Availability = availability;
        }

        public static ProductAvailabilityDTO ProductAvailabilityDTOFactory(long id, int availability)
        {
            return new ProductAvailabilityDTO(id, availability);
        }
    }
}
