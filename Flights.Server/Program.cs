using Microsoft.OpenApi.Models;

namespace Flights.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddServer(new OpenApiServer
                {
                    Description = "Development server",
                    Url = "https://localhost:7076"
                }); // Add a server URL for Swagger UI to connect with angular app
            }); // Register Swagger generator

            var app = builder.Build();

            app.UseCors(builder => builder.WithOrigins("*")); // Allow requests from any origin (for development purposes only) 

            app.UseSwagger().UseSwaggerUI(); // Enable the Swagger middleware and UI

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
