using ApiGateway.Models.ExternalAPI.Entity;

namespace ApiGateway.Models.ExternalAPI.Response
{
    public class GetSpecificProductsAvailabilitiesApiResponse
    {
        public List<ProductAvailabilityApiDTO> ProductsAvailabilities { get; set; }
    }
}
