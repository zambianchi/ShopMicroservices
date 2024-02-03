using Microsoft.EntityFrameworkCore;
using OrdiniService.Context;
using OrdiniService.RabbitManager;
using OrdiniService.Services;
using OrdiniService.Services.Int;
using OrdiniService.Settings;
using OrdiniService.Settings.Int;
using OrdiniService.SubServices;
using OrdiniService.SubServices.Int;
using ShopCommons.Models;
using ShopCommons.Services;
using ShopCommons.Services.Int;
using System.Text.Json;

namespace OrdiniService
{
    public class Program
    {
        private readonly IServiceProvider _serviceProvider;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderSubService, OrderSubService>();

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
            builder.Services.AddOpenApiDocument();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionString"])); // Dall'environment del docker compose

            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<OrderContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Avvio l'ascolto di RabbitMQ
            var rabbitMQService = app.Services.GetService<IRabbitServiceHelper>();
            rabbitMQService.StartListening(async request => await RabbitManagerMessageHelper.ManageMessage(app, request));

            app.Run();
        }
    }
}
