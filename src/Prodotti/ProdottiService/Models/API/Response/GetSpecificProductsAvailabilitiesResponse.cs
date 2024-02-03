using ProdottiService.Models.API.Entity;

namespace ProdottiService.Models.API.Response
{
    public class GetSpecificProductsAvailabilitiesResponse
    {
        public List<ProductAvailabilityDTO> ProductsAvailabilities { get; set; }

        public GetSpecificProductsAvailabilitiesResponse(List<ProductAvailabilityDTO> productsAvailabilities)
        {
            this.ProductsAvailabilities = productsAvailabilities;
        }

        public static GetSpecificProductsAvailabilitiesResponse GetSpecificProductsAvailabilitiesResponseFactory(List<ProductAvailabilityDTO> productsAvailabilities)
        {
            return new GetSpecificProductsAvailabilitiesResponse(productsAvailabilities);
        }
    }
}
