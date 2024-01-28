
using Microsoft.EntityFrameworkCore;
using ProdottiService.Context;
using ProdottiService.Services;
using ProdottiService.Services.Int;

namespace ProdottiService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddTransient<IProductService, ProductService>();

            builder.Services.AddControllers();
            builder.Services.AddOpenApiDocument();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ProductsContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionString"])); // Dall'environment del docker compose

            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ProductsContext>();
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
