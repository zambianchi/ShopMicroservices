using ApiGateway.Settings.Int;

namespace ApiGateway.Settings
{
    public class EnvironmentSettings : IEnvironmentSettings
    {
        public string EnvironmentName_OrderRabbitMQQueue { get; set; }
        public string EnvironmentName_ProductRabbitMQQueue { get; set; }

        public EnvironmentSettings(string orderRabbitMQQueue, string productRabbitMQQueue)
        {
            this.EnvironmentName_OrderRabbitMQQueue = orderRabbitMQQueue;
            this.EnvironmentName_ProductRabbitMQQueue = productRabbitMQQueue;
        }
    }
}
