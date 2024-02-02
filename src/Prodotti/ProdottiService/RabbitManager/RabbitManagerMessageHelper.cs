using ProdottiService.Models.API.Request;
using ProdottiService.Services.Int;
using ShopCommons.Models;
using System.Text.Json;

namespace ProdottiService.RabbitManager
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
                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_PRODUCTS_LIST:
                        var requestGetSpecificProducts = JsonSerializer.Deserialize<RabbitMQMessageRequest<List<long>>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var productService = serviceScope.ServiceProvider.GetRequiredService<IProductService>();
                            var result = await productService.GetSpecificProducts(requestGetSpecificProducts.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(result);
                        }

                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_GET_PRODUCT:
                        var requestParsedGetProduct = JsonSerializer.Deserialize<RabbitMQMessageRequest<long>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var productService = serviceScope.ServiceProvider.GetRequiredService<IProductService>();
                            var result = await productService.GetProduct(requestParsedGetProduct.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(result);
                        }

                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_CREATE_PRODUCT:
                        var requestParsedCreateProduct = JsonSerializer.Deserialize<RabbitMQMessageRequest<CreateProductRequest>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var productService = serviceScope.ServiceProvider.GetRequiredService<IProductService>();
                            var result = await productService.CreateProduct(requestParsedCreateProduct.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(result);
                        }

                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_DELETE_PRODUCT:
                        var requestParsedDeleteProduct = JsonSerializer.Deserialize<RabbitMQMessageRequest<long>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var productService = serviceScope.ServiceProvider.GetRequiredService<IProductService>();
                            await productService.DeleteProduct(requestParsedDeleteProduct.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(true);
                        }

                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_EDIT_PRODUCT:
                        var requestParsedEditProduct = JsonSerializer.Deserialize<RabbitMQMessageRequest<EditProductRequest>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var productService = serviceScope.ServiceProvider.GetRequiredService<IProductService>();
                            var result = await productService.EditProduct(requestParsedEditProduct.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(result);
                        }

                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_EDIT_PRODUCT_AVAILABLE_AMOUNT:
                        var requestParsedEditProductAvailableAmount = JsonSerializer.Deserialize<RabbitMQMessageRequest<EditProductAvailableAmountRequest>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var productService = serviceScope.ServiceProvider.GetRequiredService<IProductService>();
                            var result = await productService.EditProductAvailableAmount(requestParsedEditProductAvailableAmount.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(result);
                        }

                    case ShopCommons.Models.Enum.RabbitMQMessageType.REQUEST_EDIT_PRODUCTS_AVAILABLE_AMOUNT:
                        var requestParsedEditProductsAvailableAmount = JsonSerializer.Deserialize<RabbitMQMessageRequest<EditProductsAvailableAmountRequest>>(request);
                        using (var serviceScope = app.Services.CreateScope())
                        {
                            var productService = serviceScope.ServiceProvider.GetRequiredService<IProductService>();
                            await productService.EditProductsAvailableAmount(requestParsedEditProductsAvailableAmount.MessageData, new CancellationToken());
                            return JsonSerializer.Serialize(true);
                        }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
