using ApiGateway.Settings.Int;
using ApiGateway.SubService.Int;
using ShopCommons.Services.Int;

namespace ApiGateway.SubService
{
    public partial class ApiGatewaySubService : IApiGatewaySubService
    {
        private readonly IRabbitServiceHelper _rabbitServiceHelper;
        private readonly IEnvironmentSettings _environmentSettings;

        public ApiGatewaySubService(IRabbitServiceHelper rabbitServiceHelper, IEnvironmentSettings environmentSettings)
        {
            this._rabbitServiceHelper = rabbitServiceHelper;
            this._environmentSettings = environmentSettings;
        }
    }
}
