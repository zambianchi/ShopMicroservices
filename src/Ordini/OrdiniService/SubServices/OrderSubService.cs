using OrdiniService.Models.ExternalAPI.Request;
using OrdiniService.Settings.Int;
using OrdiniService.SubServices.Int;
using ShopCommons.Helper;
using ShopCommons.Models;
using ShopCommons.Services.Int;

namespace OrdiniService.SubServices
{
    public class OrderSubService : IOrderSubService
    {
        private readonly IRabbitServiceHelper _rabbitServiceHelper;
        private readonly IEnvironmentSettings _environmentSettings;

        public OrderSubService(IRabbitServiceHelper rabbitServiceHelper, IEnvironmentSettings environmentSettings)
        {
            this._rabbitServiceHelper = rabbitServiceHelper;
            this._environmentSettings = environmentSettings;
        }

        /// <summary>
        /// Riporta la vendita di specifici prodotti
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ReportSoldProducts(EditProductsAvailableAmountRequestApiDTO request, CancellationToken cancellationToken)
        {
            try
            {
                // Invio segnalazione di prodotti venduti
                var requestOrder = RabbitMQMessageRequest<EditProductsAvailableAmountRequestApiDTO>.RabbitMQMessageRequest_EDIT_EDIT_PRODUCTS_AVAILABLE_AMOUNT_Factory(request);

                // Riporto variazione quantità per i prodotti
                this._rabbitServiceHelper.EnqueueMessage(this._environmentSettings.EnvironmentName_ProductRabbitMQQueue, requestOrder);
            }
            catch (Exception ex) { }
        }
    }
}
