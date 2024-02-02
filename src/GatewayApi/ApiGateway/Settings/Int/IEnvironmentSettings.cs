namespace ApiGateway.Settings.Int
{
    public interface IEnvironmentSettings
    {
        public string EnvironmentName_OrderRabbitMQQueue { get; set; }
        public string EnvironmentName_ProductRabbitMQQueue { get; set; }
    }
}
