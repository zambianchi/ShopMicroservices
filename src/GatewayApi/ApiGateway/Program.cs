
using ApiGateway.Services;
using ApiGateway.Services.Int;
using ApiGateway.SubService;
using ApiGateway.SubService.Int;
using ShopCommons.Services.Int;
using ShopCommons.Services;
using ApiGateway.Settings.Int;
using ApiGateway.Settings;
using System.Reflection;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IApiGatewayOrderService, ApiGatewayOrderService>();
            builder.Services.AddScoped<IApiGatewayProductService, ApiGatewayProductService>();
            builder.Services.AddScoped<IApiGatewaySubService, ApiGatewaySubService>();
            builder.Services.AddScoped<IApiGatewayOrdiniSubService, ApiGatewaySubService>();
            builder.Services.AddScoped<IApiGatewayProdottiSubService, ApiGatewaySubService>();

            builder.Services.AddSingleton<IRabbitServiceHelper, RabbitServiceHelper>(x =>
                new RabbitServiceHelper(
                    builder.Configuration["RabbitMQHostname"],
                    Convert.ToInt32(builder.Configuration["RabbitMQPort"]),
                    builder.Configuration["RabbitMQUsername"],
                    builder.Configuration["RabbitMQPassword"],
                    builder.Configuration["RabbitMQQueueName"]
            ));

            builder.Services.AddSingleton<IEnvironmentSettings, EnvironmentSettings>(x =>
                new EnvironmentSettings(builder.Configuration["RabbitMQQueueNameOrders"], builder.Configuration["RabbitMQQueueNameProducts"]));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Avvio l'ascolto di RabbitMQ
            var rabbitMQService = app.Services.GetService<IRabbitServiceHelper>();
            rabbitMQService.StartListeningRpc();

            app.Run();
        }
    }
}
