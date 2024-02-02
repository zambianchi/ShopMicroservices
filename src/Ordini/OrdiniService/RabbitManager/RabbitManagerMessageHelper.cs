using OrdiniService.Models.API.Request;
using OrdiniService.Services.Int;
using ShopCommons.Models;
using System.Text.Json;

namespace OrdiniService.RabbitManager
{
    public class RabbitManagerMessageHelper
    {
        public static async Task<string> ManageMessage(WebApplication app, string request)
        {
            try
            {
                var message = JsonSerializer.Deserialize<RabbitMQMessageRequest<dynamic>>(request);

                // Tipi messaggi RabbitMQ
                switch (message.MessageType)
                {
                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_ORDER_DETAILS:
                        var requestParsedGetOrder = JsonSerializer.Deserialize<RabbitMQMessageRequest<long>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var orderService = serviceScope.ServiceProvider.GetRequiredService<IOrderService>();
                            var result = await orderService.GetOrder(requestParsedGetOrder.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(result);
                        }

                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_CREATE_ORDER:
                        var requestParsedCreateOrder = JsonSerializer.Deserialize<RabbitMQMessageRequest<CreateOrderRequest>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var orderService = serviceScope.ServiceProvider.GetRequiredService<IOrderService>();
                            var result = await orderService.CreateOrder(requestParsedCreateOrder.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(result);
                        }

                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_DELETE_ORDER:
                        var requestParsedDeleteOrder = JsonSerializer.Deserialize<RabbitMQMessageRequest<long>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var orderService = serviceScope.ServiceProvider.GetRequiredService<IOrderService>();
                            await orderService.DeleteOrder(requestParsedDeleteOrder.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(true);
                        }
                }

                return string.Empty;
            }
            catch
            {
                throw;
            }
        }
    }
}
