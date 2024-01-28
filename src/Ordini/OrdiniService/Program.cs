using Microsoft.EntityFrameworkCore;
using OrdiniService.Context;
using OrdiniService.Services;
using OrdiniService.Services.Int;
using ShopCommons.Services;
using ShopCommons.Services.Int;

namespace OrdiniService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddTransient<IOrderService, OrderService>();

            builder.Services.AddSingleton<IRabbitServiceHelper, RabbitServiceHelper>(x =>
                new RabbitServiceHelper(
                    builder.Configuration["RabbitMQHostname"],
                    Convert.ToInt32(builder.Configuration["RabbitMQPort"]),
                    builder.Configuration["RabbitMQUsername"],
                    builder.Configuration["RabbitMQPassword"],
                    builder.Configuration["RabbitMQQueueName"]
            ));

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

            app.Run();
        }
    }
}
