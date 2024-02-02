using OrdiniService.Models.ExternalAPI.Request;

namespace OrdiniService.SubServices.Int
{
    public interface IOrderSubService
    {
        Task ReportSoldProducts(EditProductsAvailableAmountRequestApiDTO request, CancellationToken cancellationToken);
    }
}
