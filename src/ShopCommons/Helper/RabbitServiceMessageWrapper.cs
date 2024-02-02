using ShopCommons.Models;
using ShopCommons.Services.Int;
using System.Text;
using System.Text.Json;

namespace ShopCommons.Helper
{
    public class RabbitServiceMessageWrapper
    {
        public static async Task<O> SendRPC<T, O>(IRabbitServiceHelper rabbitServiceHelper, string queueNameDestination, RabbitMQMessageRequest<T> messageData, CancellationToken cancellationToken)
        {
            var response = await rabbitServiceHelper.SendRPC<T>(queueNameDestination, messageData, cancellationToken);
            return JsonSerializer.Deserialize<O>(Encoding.UTF8.GetString(response));
        }
    }
}
